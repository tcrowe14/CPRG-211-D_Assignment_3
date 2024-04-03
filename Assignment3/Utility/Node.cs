namespace Assignment3
{
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