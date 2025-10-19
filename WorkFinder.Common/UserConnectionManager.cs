using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Common
{
    public class UserConnectionManager
    {
        private static readonly ConcurrentDictionary<Guid, string> _connections = new();

        public void AddConnection(Guid userId, string connectionId)
        {
            _connections[userId] = connectionId;
        }

        public void RemoveConnection(string connectionId)
        {
            var item = _connections.FirstOrDefault(x => x.Value == connectionId);
            _connections.TryRemove(item.Key, out _);
        }

        public string? GetConnectionId(Guid userId)
        {
            return _connections.TryGetValue(userId, out var connectionId) ? connectionId : null;
        }
    }
}
