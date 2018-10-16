using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewDictionary.Entity;
using NewDictionary.Interfaces;

namespace NewDictionary{
    public class CreateModel : PageModel
    {
private readonly IWordRepository _wordRepository;

        public CreateModel(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
        [BindProperty]
        public Word Word {get;set;}
        public IActionResult OnGet()
        {
            _wordRepository.CreateIndexAsync();
            // TODO remove. For quick testing.
            Word = new Word();

            return Page();
        }


        #region snippet_OnPostAsync
        public async Task<IActionResult> OnPostAsync()
        {
            Word.Id  = Guid.NewGuid().ToString();
            //if (!ModelState.IsValid)
            {
                await _wordRepository.AddWord(Word); 
                return RedirectToPage("./Home"); 
            }
                return Page();
        }
        #endregion
    }
}