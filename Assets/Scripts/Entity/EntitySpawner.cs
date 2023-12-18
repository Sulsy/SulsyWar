using System.Collections;
using System.Collections.Generic;
using Core;
using Unity.VisualScripting;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
   public List<Entity> entityPrefabs;
   public List<EntityData> entityDats;
   public Entity CreateEntity(Tile tile,int entityDataId,Colors colors,int id)
   {
      Debug.Log(entityDataId);

      Entity entity = null;
      var instantiate = Instantiate(entityPrefabs[entityDataId],transform);
      entity = entityDataId switch
      {
         0 => instantiate.GetComponent<Archer>(),
         1 => instantiate.GetComponent<Warrior>(),
         _ => null
      };

      if (entity != null)
      {
         entity.Initialization(entityDats[0], tile.PositionTile,tile.transform.position, colors, id);
      }
      return entity;
   }

   public int GetEntityPrice(int typeEntity)
   {
      return entityPrefabs[typeEntity].Price;
   }
}
