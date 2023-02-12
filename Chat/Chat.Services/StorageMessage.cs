using ChatService.Context.Models;

namespace ChatService.Services;

public class StorageMessage
{
	private readonly List<ChatMessage> _storage;
	private readonly int _capacity;

	public StorageMessage(int capacity)
	{
		_capacity = capacity;
		_storage = new List<ChatMessage>();
	}

	public IReadOnlyCollection<ChatMessage> GetAllMessage()
	{
		return _storage.AsReadOnly();
	}

	public void Add(ChatMessage message)
	{
		if (_storage.Count == _capacity)
			_storage.RemoveAt(_capacity - 1);

		_storage.Add(message);
	}
}