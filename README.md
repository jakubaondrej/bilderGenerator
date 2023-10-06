# bilderGenerator
generator of the object builder

## How to use
1. Insert required object for which the builder should be created to the project (to Directory 'ClassesForCreateBuilder')
2. Replace 'MyObject' by required object name in Program
```C#
string generatorClass = BuilderGenerator.BuilderGenerator.Generate(typeof(MyObject),false);
```
3. Run application

## example of output

```C#
public class MyObjectBuilder
{
        private MyObject _myObject;
        private MyObjectBuilder()
        {
                _myObject = new MyObject();
        }

        public static MyObjectBuilder Create() => new MyObjectBuilder();

        public MyObject Build()
        {
                return _myObject;
        }

        public MyObjectBuilder WithName(string name)
        {
                _myObject.Name = name;
                return this;
        }

        public MyObjectBuilder WithValue(string value)
        {
                _myObject.Value = value;
                return this;
        }

}
```
