using System;
using System.Collections.Generic;

namespace MilitaryHierarchy
{
    // Інтерфейси
    public interface ISoldier
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
    }

    public interface IPrivate : ISoldier
    {
        double Salary { get; }
    }

    public interface ILeutenantGeneral : ISoldier
    {
        IEnumerable<IPrivate> Privates { get; }
    }

    public interface ISpecialisedSoldier : ISoldier
    {
        string Corps { get; }
    }

    public interface IEngineer : ISpecialisedSoldier
    {
        IEnumerable<Repair> Repairs { get; }
    }

    public interface ICommando : ISpecialisedSoldier
    {
        IEnumerable<Mission> Missions { get; }
    }

    public interface ISpy : ISoldier
    {
        int CodeNumber { get; }
    }

    // Класи
    public abstract class Soldier : ISoldier
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        protected Soldier(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName} Id: {Id}";
        }
    }

    public class Private : Soldier, IPrivate
    {
        public double Salary { get; }

        public Private(int id, string firstName, string lastName, double salary)
            : base(id, firstName, lastName)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return base.ToString() + $" Salary: {Salary:F2}";
        }
    }

    public class LeutenantGeneral : Soldier, ILeutenantGeneral
    {
        private readonly List<IPrivate> _privates;

        public IEnumerable<IPrivate> Privates => _privates;

        public LeutenantGeneral(int id, string firstName, string lastName, double salary)
            : base(id, firstName, lastName)
        {
            _privates = new List<IPrivate>();
        }

        public void AddPrivate(IPrivate @private)
        {
            _privates.Add(@private);
        }

        public override string ToString()
        {
            var privatesString = Privates != null && Privates.GetEnumerator().MoveNext()
                ? "Privates:\n" + string.Join("\n", Privates)
                : string.Empty;

            return base.ToString() + $" Salary: {Salary:F2}\n" + privatesString;
        }
    }

    public abstract class SpecialisedSoldier : Soldier, ISpecialisedSoldier
    {
        private readonly string _corps;

        public string Corps => _corps;

        protected SpecialisedSoldier(int id, string firstName, string lastName, string corps)
            : base(id, firstName, lastName)
        {
            if (corps != "Airforces" && corps != "Marines")
                throw new ArgumentException("Invalid corps");
            _corps = corps;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nCorps: {Corps}";
        }
    }

    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly List<Repair> _repairs;

        public IEnumerable<Repair> Repairs => _repairs;

        public Engineer(int id, string firstName, string lastName, double salary, string corps)
            : base(id, firstName, lastName, corps)
        {
            _repairs = new List<Repair>();
        }

        public void AddRepair(Repair repair)
        {
            _repairs.Add(repair);
        }

        public override string ToString()
        {
            var repairsString = Repairs != null && Repairs.GetEnumerator().MoveNext()
                ? "Repairs:\n" + string.Join("\n", Repairs)
                : string.Empty;

            return base.ToString() + $" Salary: {Salary:F2}\n" + repairsString;
        }
    }

    public class Commando : SpecialisedSoldier, ICommando
    {
        private readonly List<Mission> _missions;

        public IEnumerable<Mission> Missions => _missions;

        public Commando(int id, string firstName, string lastName, double salary, string corps)
            : base(id, firstName, lastName, corps)
        {
            _missions = new List<Mission>();
        }

        public void AddMission(Mission mission)
        {
            _missions.Add(mission);
        }

        public override string ToString()
        {
            var missionsString = Missions != null && Missions.GetEnumerator().MoveNext()
                ? "Missions:\n" + string.Join("\n", Missions)
                : string.Empty;

            return base.ToString() + $" Salary: {Salary:F2}\n" + missionsString;
        }
    }

    public class Spy : Soldier, ISpy
    {
        public int CodeNumber { get; }

        public Spy(int id, string firstName, string lastName, int codeNumber)
            : base(id, firstName, lastName)
        {
            CodeNumber = codeNumber;
        }

        public override string ToString()
        {
            return base.ToString() + $" CodeNumber: {CodeNumber}";
        }
    }

    // Допоміжні класи
    public class Repair
    {
        public string Part { get; }
        public int Hours { get; }

        public Repair(string part, int hours)
        {
            Part = part;
            Hours = hours;
        }

        public override string ToString()
        {
            return $"Part: {Part} Hours: {Hours}";
        }
    }

    public class Mission
    {
        public string CodeName { get; }
        public string State { get; private set; }

        public Mission(string codeName, string state)
        {
            CodeName = codeName;
            State = state;
        }

        public void CompleteMission()
        {
            State = "Finished";
        }

        public override string ToString()
        {
            return $"CodeName: {CodeName} State: {State}";
        }
    }

    class Program
    {
        static void Main()
        {
            var privates = new Dictionary<int, IPrivate>();
            var generals = new Dictionary<int, ILeutenantGeneral>();
            var engineers = new Dictionary<int, IEngineer>();
            var commandos = new Dictionary<int, ICommando>();
            var spies = new Dictionary<int, ISpy>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var tokens = input.Split();
                if (tokens.Length == 0) continue;

                var type = tokens[0];
                var id = int.Parse(tokens[1]);
                var firstName = tokens[2];
                var lastName = tokens[3];
                double salary = 0;

                switch (type)
                {
                    case "Private":
                        salary = double.Parse(tokens[4]);
                        var @private = new Private(id, firstName, lastName, salary);
                        privates[id] = @private;
                        break;

                    case "LeutenantGeneral":
                        salary = double.Parse(tokens[4]);
                        var leutenantGeneral = new LeutenantGeneral(id, firstName, lastName, salary);
                        foreach (var privateId in tokens.Skip(5).Select(int.Parse))
                        {
                            if (privates.TryGetValue(privateId, out var privateSoldier))
                            {
                                leutenantGeneral.AddPrivate(privateSoldier);
                            }
                        }
                        generals[id] = leutenantGeneral;
                        break;

                    case "Engineer":
                        salary = double.Parse(tokens[4]);
                        var corps = tokens[5];
                        try
                        {
                            var engineer = new Engineer(id, firstName, lastName, salary, corps);
                            for (int i = 6; i < tokens.Length; i += 2)
                            {
                                var part = tokens[i];
                                var hours = int.Parse(tokens[i + 1]);
                                engineer.AddRepair(new Repair(part, hours));
                            }
                            engineers[id] = engineer;
                        }
                        catch (ArgumentException)
                        {
                            // Invalid corps, skip
                        }
                        break;

                    case "Commando":
                        salary = double.Parse(tokens[4]);
                        corps = tokens[5];
                        try
                        {
                            var commando = new Commando(id, firstName, lastName, salary, corps);
                            for (int i = 6; i < tokens.Length; i += 2)
                            {
                                var missionCodeName = tokens[i];
                                var missionState = tokens[i + 1];
                                if (missionState != "inProgress" && missionState != "Finished")
                                {
                                    continue; // Skip invalid mission state
                                }
                                commando.AddMission(new Mission(missionCodeName, missionState));
                            }
                            commandos[id] = commando;
                        }
                        catch (ArgumentException)
                        {
                            // Invalid corps, skip
                        }
                        break;

                    case "Spy":
                        var codeNumber = int.Parse(tokens[4]);
                        var spy = new Spy(id, firstName, lastName, codeNumber);
                        spies[id] = spy;
                        break;
                }
            }

            foreach (var @private in privates.Values)
            {
                Console.WriteLine(@private);
            }

            foreach (var general in generals.Values)
            {
                Console.WriteLine(general);
            }
        }
    }
}
