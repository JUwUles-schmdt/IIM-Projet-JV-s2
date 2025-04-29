using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace A1_24_25
{
    public class ItemController : MonoBehaviour
    {
        [field :SerializeField] public ItemDatabase.CATEGORY Category { get; private set; }
        [SerializeField] private int _id;
        [SerializeField] private bool _random;
        private ItemData _data;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;


        private void Awake()
        {
            TryGetComponent(out _spriteRenderer);
            TryGetComponent(out _collider);
        }

        private void Start()
        {
            if (Category == ItemDatabase.CATEGORY.NONE)
                Category = (ItemDatabase.CATEGORY)Random.Range(1, Enum.GetNames(typeof(ItemDatabase.CATEGORY)).Length);

            _data = DatabaseManager.Instance.GetItemData(Category, _id, _random); //DatabaseManager.Instance.GetData(this, _id) as ItemData;
            Init();
        }

        private void Init()
        {
            name = _data.label;
            _spriteRenderer.sprite = _data.sprite;
            _spriteRenderer.color = _data.color;
        }

        private void OnEffect()
        {
            Debug.Log($"{_data.label} : {_data.value} {Category.ToString().ToLower()}");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnEffect();
                gameObject.SetActive(false);
            }
        }
    }
}
