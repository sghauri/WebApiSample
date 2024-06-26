﻿namespace WebApiSample.Models
{
    public class TodoItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime? LastUpdatedDate { get; set;}
        public string Status { get; set; }
    }
}
