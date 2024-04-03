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

    public class SLL : ILinkedListADT
    {
        public Node<User> Head { get; set; }
        public Node<User> Tail { get; set; }
        private int count;
        public SLL()
        {
            // Set head and tail to null initially
            Head = null;
            Tail = null;
            count = 0;
        }

        public bool IsEmpty()
        {
            // If count is 0 then list is empty
            if (count == 0)
            {
                return true;
            }
            return false;
        }

        public void Clear()
        {
            // Reset all values
            Head = null;
            Tail = null;
            count = 0;
        }

        public void AddLast(User value)
        {
            // Create new node with value
            Node<User> node = new Node<User>(value);
            // If head is null (empty list) then set head to node
            if (Head == null)
            {
                Head = node;
            }
            else
            // Else set tail to node
            {
                Tail.Next = node;
            }
            Tail = node;
            // Update count
            count++;
        }

        public void AddFirst(User value)
        {
            // Create new node with value
            Node<User> node = new Node<User>(value);
            // If head is null (empty list) set head and tail to node
            if (Head == null)
            {
                Head = node;
                Tail = node;
            }
            else
            // Else set head of node to current head and set head to node
            {
                node.Next = Head;
                Head = node;
            }
            // Update count
            count++;
        }

        public void Add(User value, int index)
        {
            // If index > count throw exception
            if (index > count)
            {
                throw new IndexOutOfRangeException();
            }
            // Throw if index is negative
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            // If index == 0 then just use AddFirst()
            if (index == 0)
            {
                AddFirst(value);
                return;
            }
            else
            {
                // Start at head and set current to index
                Node<User> current = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                // Create node with value
                Node<User> node = new Node<User>(value);
                node.Next = current.Next;
                current.Next = node;

                if (current == Tail)
                {
                    Tail = node;
                }

                // Update count
                count++;
            }
        }

        public void Replace(User value, int index)
        {
            // If index > count throw exception
            if (index > count)
            {
                throw new IndexOutOfRangeException();
            }
            // Remove at index and add at index
            Remove(index);
            Add(value, index);
        }

        public int Count()
        {
            return count;
        }

        public void RemoveFirst()
        {
            // Set head to next
            if (Head != null)
            {
                Head = Head.Next;
                if (Head == null)
                {
                    Tail = null;
                }
                // Decrease count
                count--;
            }
            // Throw exception if list is empty
            else
            {
                throw new CannotRemoveException();
            }
        }

        public void RemoveLast()
        {
            // Throw exception if list is empty
            if (Head == null)
            {
                throw new CannotRemoveException();
            }
            // If 1 node then empty list
            if (Head.Next == null)
            {
                Head = null;
                Tail = null;
            }
            else
            // Remove last
            {
                Node<User> current = Head;
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }

                current.Next = null;
                Tail = current;
            }
            // Decrease count
            count--;
        }

        public void Remove(int index)
        {
            // Throw if index > count
            if (index > count)
            {
                throw new IndexOutOfRangeException();
            }
            // Throw if index is negative
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            // If index is first then just use RemoveFirst()
            if (index == 0)
            {
                RemoveFirst();
                return;
            }
            // If index is last then just use RemoveLast()
            if (index == count - 1)
            {
                RemoveLast();
                return;
            }
            // Go to current index and remove
            Node<User> current = Head;
            for (int i = 0; i < index - 1; i++)
            {
                current = current.Next;
            }
            current.Next = current.Next.Next;
            // Decrease count
            count--;
        }

        public User GetValue(int index)
        {
            // Throw if index > count
            if (index > count)
            {
                throw new IndexOutOfRangeException();
            }
            // Go to index
            Node<User> current = Head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            // Return value at index
            return current.Data;
        }

        public int IndexOf(User value)
        {
            // Go through list and if found return index
            Node<User> current = Head;
            int i = 0;
            while (current != null)
            {
                if (current.Data.Equals(value))
                {
                    return i;
                }
                current = current.Next;
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
            if (Head == null || Head.Next == null)
            {
                // If the list is empty or contains only one element, it's already sorted
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
            // Create array
            User[] users = new User[Count()];
            // Start at head
            Node<User> current = Head;
            int i = 0;
            while (current != null)
            {
                users[i] = current.Data;
                current = current.Next;
                i++;
            }
            return users;
        }
    }
}