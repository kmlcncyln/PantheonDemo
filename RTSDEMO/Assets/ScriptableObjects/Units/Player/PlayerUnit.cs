using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KC.RTS.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]

    public class PlayerUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        public void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveUnit(Vector2 _destination)
        {
            navAgent.SetDestination(_destination);
        }
    }

}