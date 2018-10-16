using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NewDictionary.Entity;
using NewDictionary.Interfaces;

namespace NewDictionary.DataAccess{
    public class WordRepository : IWordRepository
    {
        private readonly WordContext _context;

        public WordRepository(IOptions<Settings> settings){
            _context = new WordContext(settings);
        }
        public async Task AddWord(Word item)
        {
            try
            {
                await _context.Words.InsertOneAsync(item);
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<string> CreateIndexAsync()
        {
            try
            {
                IndexKeysDefinition<Word> keys = Builders<Word>
                .IndexKeys
                .Ascending(item => item.Id)
                .Ascending(item => item.English);
                return await _context.Words.Indexes.CreateOneAsync(new CreateIndexModel<Word>(keys));   
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<IEnumerable<Word>> GetAllWords()
        {
            try{
                return await _context.Words.Find(_ => true).ToListAsync();
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task<Word> GetWord(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Words.Find(word => word.Id == id || word.InternalId == internalId)
                        .FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
        }

        // TODO: SearchText option set to English. Need to modify this later to make multiple option
        public async Task<IEnumerable<Word>> GetWordDetailsAsync(string searchText)
        {
            try
            {
                var query = _context.Words.Find(word => word.English.Contains(searchText));
                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<bool> RemoveAllWords()
        {
            try
            {
                DeleteResult actionResult = await _context.Words.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveWordAsync(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Words.DeleteOneAsync(
                     Builders<Word>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<bool> UpdateWord(string id, string body)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateWordDocument(string id, string body)
        {
            throw new System.NotImplementedException();
        }
        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}