using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavMeshPlus.Components;
using UnityEngine;

namespace DTerrain
{
    public class BasicPaintableLayer : PaintableLayer<PaintableChunk>
    {
        [SerializeField] private NavMeshSurface navmesh;
        //CHUNK SIZE X!!!!
        public virtual void Start()
        {
            SpawnChunks();
            InitChunks();
            navmesh.BuildNavMesh();
        }

        public virtual void Update()
        {
        }
    }
}
