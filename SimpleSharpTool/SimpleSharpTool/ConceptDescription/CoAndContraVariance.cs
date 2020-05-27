using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSharpTool.Core
{
    public class CoAndContraVariance
    {
        public void Variance()
        {
            Animal A = new Animal();
            Dog D = new Dog();

            A = D;//协变
            D = (Dog)A;//逆变

            List<Animal> AList = new List<Animal>();
            List<Dog> DList = new List<Dog>();

            AList = DList.Select(d=>(Animal)d).ToList();

            IEnumerable<Dog> someDogs = new List<Dog>();
            
            //Animal 此处为一个Out修饰的协变对象
            IEnumerable<Animal> someAnimals = someDogs;
        }
    }

    public class Animal { }

    public class Dog:Animal { }
}
