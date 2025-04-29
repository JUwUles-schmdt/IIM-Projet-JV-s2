using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A1_24_25
{
    public class DatabaseManager : MonoBehaviour
    {
        private static DatabaseManager _instance;
        public static DatabaseManager Instance => _instance;

        [SerializeField] private EnemyDatabase enemyDatabase;

        [SerializeField] private List<ItemDatabase> itemDatabases;
        private void Awake()    
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public EnemyData GetEnemyData(int id, bool random = false) 
            => enemyDatabase.GetData(id, random);

        public ItemData GetItemData(ItemDatabase.CATEGORY cat, int id, bool random = false)
            => itemDatabases.Find(x => x.category == cat).GetData(id, random);

        public BaseData GetData(MonoBehaviour mono, int id, bool random = false)
        {
            switch(mono)
            {
                case EnemyController:
                    return enemyDatabase.GetData(id, random);

                case ItemController ic:
                    return GetItemData(ic.Category, id, random);

            }
            return null;
        }
    }
}
