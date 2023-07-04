using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
   [SerializeField] private GameObject prefab;
   public List<EntityData> entityDats;

   public Entity CreateEntity(Tile tile,int entityDataId,Colors colors,int id)
   {
      var entityGameObject=Instantiate(prefab);
      var entity = entityGameObject.GetComponent<Entity>();
      entity.Initialization(entityDats[entityDataId],tile,colors,id);
      return entity;
   }
}
