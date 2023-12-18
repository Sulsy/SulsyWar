using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Core;
using UnityEngine;

public static class ConverterMetaData 
{
   public static FacilityMetaData ToMetaData<T>(T facility)
   {
      switch (typeof(T).ToString())
      {
         case "Entity":
            var entity = facility as Entity;
            if (entity != null) return new FacilityMetaData(entity.Position, entity.Reward-entity.Spent);
            break;
         case "Tile":
            var tile = facility as Tile;
            if (tile != null) return new FacilityMetaData(tile.PositionTile, tile.Reward);
            break;
      }
      return null;
   }

}
