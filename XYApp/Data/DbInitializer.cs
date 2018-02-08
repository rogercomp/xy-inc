using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYApp.Models;

namespace XYApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(POIContexto context)
        {
            context.Database.EnsureCreated();
            // procura por qualquer POI
            if (context.POIs.Any())  //O banco já foi populado
            {
                return;
            }
            var pois = new POI[]
            {
            new POI{NomePOI="Lanchonete",PntX=27,PntY=12},
            new POI{NomePOI="Posto",PntX=31, PntY=18},
            new POI{NomePOI="Joalheria",PntX=15,PntY=12},
            new POI{NomePOI="Floricultura",PntX=19,PntY=21},
            new POI{NomePOI="Pub",PntX=12,PntY=8},
            new POI{NomePOI="Supermercado",PntX=23,PntY=6},
            new POI{NomePOI="Churrascaria",PntX=28,PntY=2}
            };
            foreach (POI p in pois)
            {
                context.POIs.Add(p);
            }
            context.SaveChanges();
        }
    }
}
