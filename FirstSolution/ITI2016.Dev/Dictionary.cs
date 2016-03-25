using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        class Node
        {
            public readonly TKey Key;
            public TValue Value;
            public Node Next;
            public Node( TKey k )
            {
                Key = k;
            }
        }
        Node[] _buckets;
        int _count;

        int FindBucketIndex( TKey k ) => k.GetHashCode() % _buckets.Length;

        Node FindNodeInBucket( int idxBucket, TKey k )
        {
            Node candidate = _buckets[idxBucket];
            while( candidate != null )
            {
                if( candidate.Key.Equals( k ) ) break;
                candidate = candidate.Next;
            }
            return candidate;
        }

        public Dictionary()
        {
            _buckets = new Node[7];
        }

        public TValue this[TKey k]
        {
            get
            {
                // 1 - Finds the bucket index.
                int idxBucket = FindBucketIndex( k );
                // 2 - Finds the Node in the linked list where node.Key equals k.
                Node n = FindNodeInBucket( idxBucket, k );
                // 3 - if node is found returns node.Value otherwise throws a KeyNotFoundException.
                if( n != null ) return n.Value;
                throw new KeyNotFoundException();
            }

            set
            {
                // 1 - Finds the bucket index.
                // 2 - Finds the Node in the linked list where node.Key equals k.
                // 3 - if node found, updates node.Value to value.
                //     Otherwise inserts a new Node in the linked list with k and v.
                throw new NotImplementedException();
            }
        }

        public int Count => _count;

        public IEnumerable<TKey> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add( TKey k, TValue v )
        {
            // 1 - Finds the bucket index.
            // 2 - Ensures that k does not appear in the linked list of node for the bucket.
            //     If a node is found throws an InvalidArgumentException.
            // 3 - Inserts a new Node in the linked list with k and v.
        }

        public bool ContainsKey( TKey k )
        {
            throw new NotImplementedException();
        }

        public bool ContainsValue( TValue v )
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove( TKey key )
        {
            // 1 - Finds the bucket index.
            // 2 - Finds the Node in the linked list where node.Key equals k.
            // 3 - If node found, removes it and returns true.
            //     Otherwise returns false.
            throw new NotImplementedException();
        }
    }
}
