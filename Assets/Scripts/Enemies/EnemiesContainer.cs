using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    public static EnemiesContainer Instance;
    public int enemiesCurrent;
    public int enemiesTotal;
    public TextMeshProUGUI _count;
    public List<enemiesController> _countEnemies; 
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        
    }
    
    public void ActualizarContadorTotalInicial() {
        _count.text = enemiesCurrent.ToString()+" / "+ enemiesTotal;
    }

    public void AddEnemies(enemiesController count) {
        enemiesTotal++;
        _countEnemies.Add(count);
        ActualizarContadorTotalInicial();

    }
    public void RemoveEnemies(enemiesController rest) {
        _countEnemies.Remove(rest);
        enemiesCurrent++;
        ActualizarContadorTotalInicial();
    }
}

