using System;
using System.Collections.Generic;
using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

    public class Patrol : MonoBehaviour
    {

        public BehaviourTree tree;
        public NavMeshAgent agent;
        public List<Transform> patrolPoints;
        public Transform prize;
        public float patrolSpeed;

       

        void Start()
        {
            tree = new BehaviourTree("Patrol");

            BehaviourTrees.Condition checkTreasure = new BehaviourTrees.Condition(() => prize.gameObject.activeSelf);
            Leaf isPrizeActive = new Leaf("isPrizeActive", checkTreasure);

            Leaf moveToPrize = new Leaf("MoveToPrize", new ActionStrategy(() => agent.SetDestination(prize.position)));

            Sequencer findPrize = new Sequencer("FindPrize");

            findPrize.AddChildren(isPrizeActive);
            findPrize.AddChildren(moveToPrize);

            Selector baseSelector = new Selector("BaseSelector");
            
            baseSelector.AddChildren(findPrize);

            PatrolStrategy patrolStrategy = new PatrolStrategy(transform, agent, patrolPoints, patrolSpeed);
            baseSelector.AddChildren(new Leaf("Patroling", patrolStrategy));

            tree.AddChildren(baseSelector);
        }

        void Update()
        {
            tree.Process();
            Debug.Log(tree.GetCurrentChild);
        }
    }

