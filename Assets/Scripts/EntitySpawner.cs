using System.Collections;
using System.Collections.Generic;
using Core;
using Unity.VisualScripting;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
   public List<Entity> entityDats;

   public Entity CreateEntity(Tile tile,int entityDataId,Colors colors,int id)
   {
      Debug.Log(entityDataId);

      Entity entity = null;
      var instantiate = Instantiate(entityDats[entityDataId]);
      entity = entityDataId switch
      {
         0 => instantiate.GetComponent<Archer>(),
         1 => instantiate.GetComponent<Entity>(),
         _ => null
      };

      if (entity != null)
      {
         entity.Initialization(entityDats[entityDataId].Data, tile, colors, id);
      }
      return entity;
   }

   public int GetEntityPrice(int typeEntity)
   {
      return entityDats[typeEntity].price;
   }
}
