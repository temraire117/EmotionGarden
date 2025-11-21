using System.Collections.Generic;
using System.IO;
using SQLite4Unity3d;
using UnityEngine;
using EmotionGarden.Models;

public class DatabaseController : MonoBehaviour
{
    public static DatabaseController Instance { get; private set; }
    private SQLiteConnection _connection;
    private string dbName = "egDB.db";
    private int _characterId;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        string dbPath = GetDatabasePath(dbName);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        SeedItems();
        _characterId = GetOrCreateDefaultCharacter();
    }

    
    private string GetDatabasePath(string dbName)
    {
        string persistentPath = Path.Combine(Application.persistentDataPath, dbName);
        string streamingPath = Path.Combine(Application.streamingAssetsPath, dbName);

    #if UNITY_EDITOR
        bool forceReset = false;

        if (forceReset && File.Exists(persistentPath))
        {
            File.Delete(persistentPath);
        }

        if (!File.Exists(persistentPath))
        {
            File.Copy(streamingPath, persistentPath);
        }

        return persistentPath;
    #else
        if (!File.Exists(persistentPath))
        {
            if (streamingPath.Contains("://") || streamingPath.Contains(":///"))
            {
                var www = UnityEngine.Networking.UnityWebRequest.Get(streamingPath);
                www.SendWebRequest();
                while (!www.isDone) { }
                File.WriteAllBytes(persistentPath, www.downloadHandler.data);
            }
            else
            {
                File.Copy(streamingPath, persistentPath);
            }
        }
        return persistentPath;
    #endif
    }

    private void SeedItems()
    {
        var initialItems = ItemSeeder.GetInitialItems();
        foreach (var item in initialItems)
        {
            var exists = _connection.Table<Item>().Where(i => i.item_id == item.item_id).FirstOrDefault();
            if (exists == null)
            {
                _connection.Insert(item);
            }
        }
    }

    private int GetOrCreateDefaultCharacter()
    {
        var character = _connection.Table<Character>().FirstOrDefault();
        if (character != null)
            return character.Id;

        character = new Character { emotion_points = 0 };
        _connection.Insert(character);
        return character.Id;
    }


    // 보유 중인 모든 아이템 조회
    public List<Item> GetMyItems()
    {
        string sql = @"SELECT Item.* FROM Item 
                       INNER JOIN CharacterItem ON Item.item_id = CharacterItem.item_id";
        return _connection.Query<Item>(sql);
    }

    //보유 중인 타입 별 아이템 조회
    public List<Item> GetMyItemsByType(string type)
    {
        string sql = @"SELECT Item.item_id, Item.name, Item.price, Item.type
                    FROM Item
                    INNER JOIN CharacterItem 
                        ON Item.item_id = CharacterItem.item_id
                    WHERE Item.type = ?";
        return _connection.Query<Item>(sql, type);
    }

    
    // 꽃 아이템 조회
    public List<Item> GetFlowerItems()
    {
        string sql = @"SELECT Item.* 
                    FROM Item
                    WHERE type = 'flower'";
        return _connection.Query<Item>(sql);
    }
    // 특정 아이템 조회
    public Item GetMyItemById(int itemId)
    {
        return GetFlowerItems().Find(item => item.item_id == itemId);
    }

    //타입 별 아이템 조회
    public List<Item> GetAllItemsByType(string type)
    {
        string sql = @"SELECT Item.* 
                    FROM Item
                    WHERE type = ?";
        return _connection.Query<Item>(sql, type);
    }

    // 상점
    // 아이템 구매
    public void AddItem(int itemId)
    {
        var exists = _connection.Table<CharacterItem>()
                                .Where(ci => ci.item_id == itemId)
                                .FirstOrDefault();
        if (exists == null)
        {
            _connection.Insert(new CharacterItem { item_id = itemId });
        }
    }

    //상점 아이템 리스트
    public List<ItemIdPriceName> GetItemsByType(string type)
    {
        string sql = "SELECT item_id, price, name FROM Item WHERE type = ?";
        return _connection.Query<ItemIdPriceName>(sql, type);
    }

    // 감정 포인트 조회
    public int GetEmotionPoints()
    {
        var character = _connection.Table<Character>().FirstOrDefault();
        return character != null ? character.emotion_points : 0;
    }

    // 감정 포인트 업데이트
    public void UpdateEmotionPoints(int points)
    {
        var character = _connection.Table<Character>().FirstOrDefault();
        if (character != null)
        {
            character.emotion_points = points;
            _connection.Update(character);
        }
    }
    
    // 감정저장소
    // 특정 날짜 기록 1건 조회
    public Diary GetDiary(string date)
    {
        return _connection.Table<Diary>().Where(d => d.date == date).FirstOrDefault();
    }
    
    //오늘 기록 여부
    public bool HasDiaryToday()
    {
    string today = System.DateTime.Now.ToString("yyyy-MM-dd");
    var diary = _connection.Table<Diary>().Where(d => d.date == today).FirstOrDefault();
    return diary != null;
    }

    // 저장 또는 업데이트 (하루 1회)
    public void SaveDiary(string date, string mood)
    {
        var existing = _connection.Table<Diary>().Where(d => d.date == date).FirstOrDefault();

        if (existing == null)
        {
            _connection.Insert(new Diary
            {
                date = date,
                mood = mood
            });
        }
        else
        {
            existing.mood = mood;
            _connection.Update(existing);
        }
    }

    // 특정 월 전체 기록 조회
    public List<Diary> GetDiaryByMonth(int year, int month)
    {
        string prefix = $"{year}-{month:D2}";
        string sql = $"SELECT * FROM Diary WHERE date LIKE '{prefix}-%'";
        return _connection.Query<Diary>(sql);
    }


}


