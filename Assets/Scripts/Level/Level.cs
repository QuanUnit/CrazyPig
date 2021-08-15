using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level : Singleton<Level>
{
    public UnityEvent OnPlayerDied;
    public UnityEvent OnAllEnemiesDied;

    [SerializeField] private LevelGrid _grid;
    [SerializeField] private PoolObjects<Entity> _entitiesPool;

    private List<Enemy> _liveEnemies = new List<Enemy>();

    private void Start()
    {
        _grid.Initalize(LevelTemplate.DefaultTemplate);
        ArrangeContent();

        OnPlayerDied.AddListener(delegate {  });
    }

    private void ArrangeContent()
    {
        List<Cell> passablesCells = new List<Cell>(_grid.PassablesCells.Values);
        List<Entity> randomEntities = _entitiesPool.GetRandom();

        if (randomEntities.Count > passablesCells.Count)
            throw new System.Exception("Not enough space for spawn");

        for(int i = 0; i < randomEntities.Count; i++)
        {
            Cell cell = passablesCells[Random.Range(0, passablesCells.Count)];
            Entity entity = randomEntities[i];

            SpawnEntity(entity, cell);

            passablesCells.Remove(cell);
        }
    }
    
    private void SpawnEntity(Entity target, Cell cell)
    {
        Entity spawnedEntity = Instantiate(target.gameObject, cell.Position, Quaternion.identity, transform).GetComponent<Entity>();

        if (spawnedEntity.GetType() == typeof(Player))
        {
            spawnedEntity.OnDeath += delegate { OnPlayerDied?.Invoke(); };
        }

        else if (spawnedEntity.GetType() == typeof(Enemy))
        {
            _liveEnemies.Add((Enemy)spawnedEntity);
            spawnedEntity.OnDeath += delegate 
            {
                _liveEnemies.Remove((Enemy)spawnedEntity);
                if (_liveEnemies.Count == 0)
                {
                    OnAllEnemiesDied?.Invoke();
                }
            };
        }

        spawnedEntity.Warden.ChangeCell(cell);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
