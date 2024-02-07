namespace Beershop24.Generic
{
    public class DataStore<T>
    {
        //C# allows you to define generic classes, interfaces, abstract classes,
        //fields, methods, static methods, properties, events, delegates,
        //and operators using the type parameter and without the specific data type.
        //A type parameter is a placeholder for a particular type specified
        //when creating an instance of the generic type.
        // https://www.tutorialsteacher.com/csharp/csharp-generics

        private readonly T[] _data;

        public DataStore()
        {
            _data = new T[10];
        }

        public void AddOrUpdate(int index, T item)
        {
            if (index >= 0 && index < 10)
                _data[index] = item;
        }

        public T? GetData(int index) => _data.ElementAtOrDefault(index);

	}
}
