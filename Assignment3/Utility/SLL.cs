using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

// Assignment 3
// Group 1
// April 3rd 2024

namespace Assignment3
{
    class CannotRemoveException : Exception
    {
        public CannotRemoveException() { }
        public CannotRemoveException(string message) : base(message) { }
        public CannotRemoveException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class SLL : ILinkedListADT
    {
        public Node<User> Head { get; set; }
        public Node<User> Tail { get; set; }
        private int count;
        public SLL()
        {
            Head = null; // Initialize Head as null
            Tail = null; // Initialize Tail as null
            count = 0; // Initialize count as 0 (list is empty, no values)
        }

        public bool IsEmpty()
        {
            if (count == 0) // Use count if 0 then list is empty
            {
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Head = null; // Reset value to clear
            Tail = null; // Reset value to clear
            count = 0; // Reset value to clear
        }

        public void AddLast(User value)
        {
            Node<User> node = new Node<User>(value); // Create new node with value
            if (Head == null) // If list is empty then set head to node
            {
                Head = node;
            }
            else
            {
                Tail.Next = node; // Set tail to node
            }
            Tail = node;
            count++; // Increment count
        }

        public void AddFirst(User value)
        {
            Node<User> node = new Node<User>(value); // Create new node with value
            if (Head == null) // If list is empty then set head and tail to node
            {
                Head = node;
                Tail = node;
            }
            else
            {
                node.Next = Head; //Set head of node to current head 
                Head = node; // Set head to node
            }
            count++; // Increment count
        }

        public void Add(User value, int index)
        {
            if (index > count)
            {
                throw new IndexOutOfRangeException(); // If index > count throw exception
            }
            if (index < 0) // Throw if index is negative
            {
                throw new IndexOutOfRangeException();
            }
            if (index == 0)
            {
                AddFirst(value); // Use AddFirst() if index == 0
                return;
            }
            else
            {
                Node<User> current = Head; // Start at head and set current to index
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                Node<User> node = new Node<User>(value); // Create node with value
                node.Next = current.Next;
                current.Next = node;

                if (current == Tail)
                {
                    Tail = node;
                }
                count++; // Increment count
            }
        }

        public void Replace(User value, int index)
        {
            if (index > count)
            {
                throw new IndexOutOfRangeException(); // If index > count throw exception
            }
            Remove(index); // Remove at index
            Add(value, index); // Add new value at index
        }

        public int Count()
        {
            return count;
        }

        public void RemoveFirst()
        {
            if (Head != null)
            {
                Head = Head.Next; // Set head to next
                if (Head == null)
                {
                    Tail = null;
                }
                count--; // Decrement count
            }
            else
            {
                throw new CannotRemoveException(); // Throw exception if list is empty
            }
        }

        public void RemoveLast()
        {
            if (Head == null)
            {
                throw new CannotRemoveException(); // Throw exception if list is empty
            }
            if (Head.Next == null) // If only one node then empty list
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Node<User> current = Head;
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }

                current.Next = null;
                Tail = current; // Remove last
            }
            count--; // Decrement count
        }

        public void Remove(int index)
        {
            if (index > count) // Throw if index > count
            {
                throw new IndexOutOfRangeException();
            }
            if (index < 0) // Throw if index is negative
            {
                throw new IndexOutOfRangeException();
            }
            if (index == 0)
            {
                RemoveFirst(); // Use RemoveFirst() if index is first then just 
                return;
            }
            if (index == count - 1)
            {
                RemoveLast(); // Use RemoveLast() if index is last
                return;
            }
            Node<User> current = Head; // Go to current index and remove
            for (int i = 0; i < index - 1; i++)
            {
                current = current.Next;
            }
            current.Next = current.Next.Next;
            count--; // Decrement count
        }

        public User GetValue(int index)
        {
            if (index > count)
            {
                throw new IndexOutOfRangeException(); // Throw if index > count
            }
            Node<User> current = Head; // Go to index
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data; // Return value at index
        }

        public int IndexOf(User value)
        {
            Node<User> current = Head;
            int i = 0;
            while (current != null)
            {
                if (current.Data.Equals(value)) // Go through list and if found return index
                {
                    return i;
                }
                current = current.Next; // Recursively progress through the list
                i++;
            }
            return -1;
        }

        public bool Contains(User value)
        {
            // Go through list and if found return value
            Node<User> current = Head;
            while (current != null)
            {
                if (current.Data.Equals(value))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void SortByName()
        {
            if (Head == null || Head.Next == null) // If the list is empty or contains only one element, it's already sorted
            {
                return;
            }

            bool swapped;
            do
            {
                swapped = false;
                Node<User> current = Head;
                Node<User> previous = null;

                while (current != null && current.Next != null)
                {
                    if (string.Compare(current.Data.Name, current.Next.Data.Name) > 0)
                    {
                        // Swap nodes if the names are not in ascending order
                        if (previous != null)
                        {
                            previous.Next = current.Next;
                        }
                        else
                        {
                            Head = current.Next;
                        }

                        Node<User> next = current.Next;
                        current.Next = next.Next;
                        next.Next = current;

                        if (current == Tail)
                        {
                            Tail = next;
                        }

                        swapped = true;
                    }

                    previous = current;
                    current = current.Next;
                }
            } while (swapped);
        }

        public User[] NodesToArray()
        {
            User[] users = new User[Count()]; // Create new User array
            Node<User> current = Head; // Read from the first (Head)
            int i = 0;
            while (current != null) // Loop through SLL
            {
                users[i] = current.Data;
                current = current.Next;
                i++;
            }
            return users;
        }
    }
}