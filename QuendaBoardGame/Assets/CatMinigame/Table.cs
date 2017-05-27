using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Wrapper class for a table/dictionary
    /// By Elizabeth Haynes
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    public class Table<TKey, TValue>
    {
        /// <summary>
        /// Data
        /// </summary>
        Dictionary<TKey, TValue> m_data;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Size of table</param>
        public Table(int size)
        {
            m_data = new Dictionary<TKey, TValue>(size);
        }

        /// <summary>
        /// Add to table
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Add(TKey key, TValue value)
        {
            m_data.Add(key, value);
        }

        /// <summary>
        /// Get from table
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public TValue Get(TKey key)
        {
            return m_data[key];
        }

        /// <summary>
        /// Clear table
        /// </summary>
        public void Clear()
        {
            m_data.Clear();
        }

        /// <summary>
        /// Remove value with this key
        /// </summary>
        /// <param name="key">key to remove value of</param>
        public void Remove(TKey key)
        {
            m_data.Remove(key);
        }

        /// <summary>
        /// True if contains this key
        /// </summary>
        /// <param name="key">key to check</param>
        /// <returns>contains key</returns>
        public bool ContainsKey(TKey key)
        {
            return m_data.ContainsKey(key);
        }
        /// <summary>
        /// True if contains this value
        /// </summary>
        /// <param name="val">value to check</param>
        /// <returns>contains value</returns>
        public bool ContainsValue(TValue val)
        {
            return m_data.ContainsValue(val);
        }
    }


