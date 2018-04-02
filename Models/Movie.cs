﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VideoStoreApi.Models
{
    public class Movie
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Genre { get; set; }
        public string Upc { get; set; }
        public int Status { get; set; }
    }

    public class MovieTitles
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string ReturnDate { get; set; }
        public string CustFirstName { get; set; }
        public string CustLastName { get; set; }
        public long CustPhoneNumber { get; set; }

    }

    public class NewMovie
    {
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Genre { get; set; }
        public string Upc { get; set; }
    }

    public class MovieId
    {
        public long Id { get; set; }
    }
    public class Movies4Trans
    {
        public long Id { get; set; }
        public int Cost { get; set; }
        public string DueDate { get; set; }
    }
}

