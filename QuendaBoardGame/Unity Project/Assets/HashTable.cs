using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Hash Table Entry
 * By Maddy Topaz
 * */
struct Entry<TKey,TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}


/*
 * Hash Table data structure
 * By Maddy Topaz
 * */
class HashTable<TKey,TValue>
{
    private int m_size;
    private LinkedList<Entry<TKey, TValue>>[] entries;

    public HashTable(int size)
    {
        m_size = size;
        entries = new LinkedList<Entry<TKey, TValue>>[m_size];
    }
        
    //Get the bucket with the position we hashed
    protected int GetArrayPosition(TKey key)
    {
        //Get the position using hashcode from key and mod by size so we get a value within range 
        int position = key.GetHashCode() % m_size;
        return Math.Abs(position);
    }

    protected LinkedList<Entry<TKey,TValue>> GetLinkedList(int position)
    {
        //Get the linked list back at the specified position
        LinkedList<Entry<TKey, TValue>> linkedList = entries[position];
        if(linkedList == null)
        {
            //if linked list is null no list exists at that position
            linkedList = new LinkedList<Entry<TKey, TValue>>();
            entries[position] = linkedList;
        }

        return linkedList;
    }

    public TValue Find(TKey key)
    {
        //Get the position that corresponds to the key 
        int position = GetArrayPosition(key);
        //Get the linked list at that position
        LinkedList<Entry<TKey, TValue>> linkedList = GetLinkedList(position);
        //Iterate over all items in linked list
        foreach(Entry<TKey,TValue> item in linkedList)
        {
            //If key found
            if(item.Key.Equals(key))
            {
                //Return the corresponding value
                return item.Value;
            }
            else
            {
                Console.WriteLine("Value not found.");
            }
        }

        //return null
        return default(TValue);              
    }

    public bool ContainsKey(TKey key)
    {
        //Get the position that corresponds to the key 
        int position = GetArrayPosition(key);

        //Get the linked list back at the specified position
        LinkedList<Entry<TKey, TValue>> linkedList = entries[position];

        if(linkedList == null)
        {
            return false;
        }
        return true;
    }

    public void Add(TKey key, TValue value)
    {
        //Get the position that corresponds to the key 
        int position = GetArrayPosition(key);
        //Get the linked list at that position
        LinkedList<Entry<TKey, TValue>> linkedList = GetLinkedList(position);
        //Create the new key value pair
        Entry<TKey, TValue> item = new Entry<TKey, TValue> { Key = key, Value = value };
        //Add the item to the bucket (linked list)
        linkedList.AddLast(item);
    }

    public void Remove(TKey key)
    {
        //Get the position that corresponds to the key 
        int position = GetArrayPosition(key);
        //Get the linked list at that position
        LinkedList<Entry<TKey, TValue>> linkedList = GetLinkedList(position);
        bool itemFound = false;
        //Create an empty entry
        Entry<TKey, TValue> foundItem = default(Entry<TKey, TValue>);
        //for each item in the bucket (linked list)
        foreach(Entry<TKey,TValue> item in linkedList)
        {
            //If the key matched our argument
            //we found the item
            if(item.Key.Equals(key))
            {
                itemFound = true;
                foundItem = item;
            }
        }

        if(itemFound)
        {
            //remove from bucket
            linkedList.Remove(foundItem);
        }
    }

    public void Clear()
    {
        entries = new LinkedList<Entry<TKey, TValue>>[m_size];
    }

}
