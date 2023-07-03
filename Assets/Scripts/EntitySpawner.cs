using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
   [SerializeField] private GameObject prefab;
   public List<EntityData> entityDats;
   public List<Entity> entities;
   
   public void CreateEntity(Tile tile,int entityDataId,Colors colors)
   {
      var entityGameObject=Instantiate(prefab);
      var entity = entityGameObject.GetComponent<Entity>();
      entity.Initialization(entityDats[entityDataId],tile,colors,entities.Count);
      entities.Add(entity);
   }
}
