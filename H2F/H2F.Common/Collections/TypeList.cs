using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace H2F.Standard .Common.Collections
{
    public class TypeList: TypeList<object>, ITypeList
    {
    }

    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        private readonly List<Type> _typeList;

        public TypeList()
        {
            _typeList = new List<Type>();
        }

        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }
         
        public int Count {get { return _typeList.Count; } } 

        public bool IsReadOnly { get { return false; } }

       public Type this[int index]
        {
            get { return _typeList[index]; }
            set {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        public void Clear()
        {
            _typeList.Clear();
        }

        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }

        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        public int IndexOf(Type item)
        {
            CheckType(item);
            return _typeList.IndexOf(item);
        }

        public void Insert(int index, Type item)
        {
            CheckType(item);
            _typeList.Insert(index, item);
        }

        public void Remove<T>() where T : TBaseType
        {
           _typeList.Remove(typeof(T));
        }

        public bool Remove(Type item)
        {
            return _typeList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (index>=0 && index<_typeList.Count)
            {
                _typeList.RemoveAt(index);
            }
        }


        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
            {
                throw new ArgumentException("Given item is not type of " + typeof(TBaseType).AssemblyQualifiedName, "item");
            }
        }
    }
}
