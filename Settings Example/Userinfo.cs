using System;

namespace Settings_Example
{
    [Serializable]
    public class Userinfo
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public Userinfo() { }

        public Userinfo(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }
}
