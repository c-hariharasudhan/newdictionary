using System.Collections.Generic;
using System.Threading.Tasks;
using NewDictionary.Entity;

namespace NewDictionary.Interfaces
{
public interface IWordRepository
{
    Task<IEnumerable<Word>> GetAllWords();

        Task<Word> GetWord(string id);

        // query after multiple parameters
        Task<IEnumerable<Word>> GetWordDetailsAsync(string searchText);

        // add new note document
        Task AddWord(Word item);

        // remove a single document / note
        Task<bool> RemoveWordAsync(string id);

        // update just a single document / note
        Task<bool> UpdateWord(string id, string body);

        // demo interface - full document update
        Task<bool> UpdateWordDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllWords();

        // creates a sample index
        Task<string> CreateIndexAsync();
}
}