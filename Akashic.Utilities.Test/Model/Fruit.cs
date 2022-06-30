using System;

namespace Akashic.Utilities.Test.Model
{
    [Serializable]
    public class Fruit : ICloneable, IEquatable<Fruit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone(); ;
        }

        public bool Equals(Fruit other)
        {
            return (this.Id == other.Id) && (this.Name == other.Name) && (this.Group == other.Group);
        }

        public string ShowData()
        {
            return $"{Id}-{Name}";
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
