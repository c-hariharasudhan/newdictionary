using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewDictionary.Entity;
using NewDictionary.Interfaces;

namespace NewDictionary.Pages
{
    public class HomeModel : PageModel
    {
        private readonly IWordRepository _wordRepository;

        public HomeModel(IWordRepository wordRepository){
            _wordRepository = wordRepository;
        }
        [BindProperty]
        public List<Word> Words {get;set;}
        [BindProperty]
        public string SearchText {get;set;}
        public void OnGet()
        {
            Words = _wordRepository.GetAllWords().Result.ToList();
        }
    }
}
