using System;

namespace Assignment3

// Assignment 3
// Group 1
// April 3rd 2024

{
    [Serializable]
    public class Node<User>
    {
        // Node for User
        public User Data { get; set; }
        public Node<User> Next { get; set; }
        public Node(User data)
        {
            this.Data = data;
        }
    }
}