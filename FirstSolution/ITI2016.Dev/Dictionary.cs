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
                int idxBucket = FindBucketIndex( k );
                // 2 - Finds the Node in the linked list where node.Key equals k.
                Node n = FindNodeInBucket( idxBucket, k );
                // 3 - if node found, updates node.Value to value.
                //     Otherwise inserts a new Node in the linked list with k and v.
                if( n != null ) n.Value = value;
                else
                {
                    _buckets[idxBucket] = new Node( k )
                    {
                        Value = value,
                        Next = _buckets[idxBucket]
                    };
                    ++_count;
                }
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
            int idxBucket = FindBucketIndex( k );
            // 2 - Finds the Node in the linked list where node.Key equals k.
            Node n = FindNodeInBucket( idxBucket, k );
            // 3 - if node found, throws an ArgumentException.
            //     Otherwise inserts a new Node in the linked list with k and v.
            if( n != null ) throw new ArgumentException();
            else
            {
                _buckets[idxBucket] = new Node( k )
                {
                    Value = v,
                    Next = _buckets[idxBucket]
                };
                ++_count;
            }
        }

        public bool ContainsKey( TKey k ) => FindNodeInBucket( FindBucketIndex( k ), k ) != null;

        public bool ContainsValue( TValue v )
        {
            for( int i = 0; i < _buckets.Length; i++ )
            {
                Node n = _buckets[i];
                while( n != null )
                {
                    if( 
                        (n.Value != null && n.Value.Equals( v ))
                        ||
                        (n.Value == null && v == null)
                        ) return true;
                    n = n.Next;
                }
            }
            return false;
        }

        public class E : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            readonly Dictionary<TKey, TValue> _owner;
            int _idxBuket;
            Node _node;

            public E( Dictionary<TKey,TValue> d )
            {
                _owner = d;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    if( _node == null ) throw new InvalidOperationException();
                    return new KeyValuePair<TKey, TValue>( _node.Key, _node.Value );
                }
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new E( this );
        }

        public bool Remove( TKey key )
        {
            // 1 - Finds the bucket index.
            int idxBucket = FindBucketIndex( key );
            // 2 - Finds the Node in the linked list where node.Key equals k.
            Node prevNode = null;
            Node n = _buckets[idxBucket];
            while( n != null )
            {
                if( n.Key.Equals( key ) ) break;
                prevNode = n;
                n = n.Next;
            }
            // 3 - If node found, removes it and returns true.
            //     Otherwise returns false.
            if( n != null )
            {
                if( prevNode == null )
                {
                    _buckets[idxBucket] = n.Next;
                }
                else
                {
                    prevNode.Next = n.Next;
                }
                --_count;
                return true;
            }
            return false;
        }

        public bool TryGetValue( TKey k, out TValue v )
        {
            // 1 - Finds the bucket index.
            int idxBucket = FindBucketIndex( k );
            // 2 - Finds the Node in the linked list where node.Key equals k.
            Node n = FindNodeInBucket( idxBucket, k );
            // 3 - if node is found sets value to node.Value and returns true.
            //     Otherwise sets value to default(TValue) and returns false.
            if( n != null )
            {
                v = n.Value;
                return true;
            }
            v = default( TValue );
            return false;
        }
    }
}
