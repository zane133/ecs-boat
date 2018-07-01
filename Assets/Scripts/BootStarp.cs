using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BootStarp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        var entities  = entityManager.GetAllEntities();

        for (int i = 0; i < entities.Length; i++)
        {
            if (entityManager.HasComponent<Initialize>(entities[i]))
            {

                MoveStatus moveStatus = new MoveStatus();
                moveStatus.Mass = 10000;
                moveStatus.VelocityZ = 0;
                moveStatus.ThrustZ = 0;
                moveStatus.ResistanceZ = 1000;
                moveStatus.TimeZ = 0;

                entityManager.SetComponentData(entities[i], moveStatus);

                entityManager.RemoveComponent<Initialize>(entities[i]);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
