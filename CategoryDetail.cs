using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WritersInn1.Models

{
    public class CategoryDetail
    {


        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name Requird")]
        [StringLength(100,
       ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed",
       MinimumLength = 3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }

    }

    public class ContentDetail
    {
        public int ContentId { get; set; }

        [Required(ErrorMessage = "Content Name is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]



        public string ContentName { get; set; }
        [Required]
        [Range(1, 50)]


        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required(ErrorMessage = "short Description is Required")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "long Description is Required")]
        public string LongDescription { get; set; }
        public string ContentImage { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> Quantity { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "200000", ErrorMessage = "invalid word count")]
        public Nullable<int> Word_count { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "200000", ErrorMessage = "invalid Price")]
        public Nullable<decimal> Price { get; set; }


        public SelectList Categories { get; set; }
    }
}