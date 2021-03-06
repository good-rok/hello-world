using System;
using System.Threading;
//using System.Threading.Tasks.Task;
using engine;
using UnityEngine;

namespace engine
{



	public class AddBlocksJob :Job2
	{   

		public ContainerChunks.IteratorItem m_containerChunksIteratorItem;
		public ContainerMeshBlock2.IteratorItem m_containerMeshBlockIteratorItem;
		public ContainerFace m_findContainer3DFace;
		 
		public Vector3 m_position;
		public int m_blockID;

		Blocks m_currentOppositeBlocks;
		Chunk m_currentOppositeChunk;
		ContainerBlocks.IteratorItem m_currentOppositeChunkBlocksIteratorItem;
		//UnityEngine.Vector3 m_currentOppositePositionInChunk;
		//UnityEngine.Vector3 m_currentPositionInChunk;
		int m_currentMeshBlockFace;
		MeshBlock2 m_currentMeshBlock;

		//Blocks[] m_oppositesBlocks;
		//Chunk[] m_oppositesChunks;
		ContainerBlocks.IteratorItem[] m_containerBlocksIteratorItem;

		Rectangle m_rectangle;
		//DiagonalOppositeBlock m_diagonalOppositeBlock;
		//DiagonalOppositeAirBlock m_diagonalOppositeAirBlock;
		//OppositeAirBlockFace m_oppositeAirBlockFace;
		OppositeBlockFace m_oppositeBlockFace;

		 

		public AddBlocksJob(
			ref World world,
			ref Chunk chunk
		) : base (ref world,
			ref chunk,
			new UnityEngine.Vector3 (0, 0, 0), 
			new UnityEngine.Vector3 (
				chunk.m_size/ chunk.m_unitLenght, 
				chunk.m_size/ chunk.m_unitLenght, 
				chunk.m_size/ chunk.m_unitLenght)
		)
		{
			
			m_world.m_containerChunks.newIterator( ref m_containerChunksIteratorItem);
			m_world.m_containerMeshBlocks.newIterator (ref m_containerMeshBlockIteratorItem);
			m_oppositeBlockFace = new OppositeBlockFace (ref m_world, ref m_chunk);
			/**
			m_oppositesBlocks = new Blocks[10];
			m_oppositesChunks = new Chunk[10];
			m_containerBlocksIteratorItem = new Container3D<Blocks>.IteratorItem [10];

			for (int f = 0; f < 10; f++) {
				m_oppositesBlocks [f] = null;
				m_oppositesChunks [f] = null;
				m_containerBlocksIteratorItem [f] = null;
			}
			
			//m_diagonalOppositeBlock = new DiagonalOppositeBlock ();
			//m_diagonalOppositeAirBlock = new DiagonalOppositeAirBlock ();

			m_oppositeAirBlockFace = new OppositeAirBlockFace ();
**/
		}

	









		public UnityEngine.Vector3 posInChunhhk2PosInWorld(UnityEngine.Vector3 posInChunk) {
			
			UnityEngine.Vector3 currentPositionInWord;

			currentPositionInWord.x = m_chunk.m_position.x * m_chunk.m_size + posInChunk.x;
			currentPositionInWord.y = m_chunk.m_position.y * m_chunk.m_size + posInChunk.y;
			currentPositionInWord.z = m_chunk.m_position.z * m_chunk.m_size + posInChunk.z;

			return currentPositionInWord;
		}


		public void deleteFace2 (
			ref Chunk chunk,
			UnityEngine.Vector3 position,
			int blockID,
			int typeFace
		)
		{
			ContainerFace.IteratorItem containerFaceIteratorItem = null;
			Debug.Log ("jaaaaaaaaaa");
			if (chunk.m_containerMeshBlocks.getMeshBlock (
				ref chunk.m_containerMeshBlocksIteratorItem, 
				blockID, 
				ref m_currentMeshBlock)) {

				Face findFace = null;

				if (chunk.m_containerTypeFaceSwap [chunk.m_swapLOD].findContainerFace (
					ref chunk.m_containerTypeFaceIteratorItemSwap [chunk.m_swapLOD],
					typeFace, 
					false, 
					false, 
					ref m_currentMeshBlock, 
					ref m_findContainer3DFace)) {
					Debug.Log ("bbbbbbbbbbbj");

					m_findContainer3DFace.newIterator (ref containerFaceIteratorItem);
					m_findContainer3DFace.findFaceAtPosition (
						ref containerFaceIteratorItem, position, ref findFace);
					if (!object.ReferenceEquals (findFace, null)) {
						findFace.m_garbage = true;
						m_findContainer3DFace.updateMesh ();
						Debug.Log ("jcccccccccc");
						m_findContainer3DFace.updateGameObject ();
					} else {Debug.Log ("findface3 null");
					}
				}
			}

		}



		public void addFace2 (
			ref Chunk chunk,
			UnityEngine.Vector3 position,
			int blockID,
			int typeFace
		)
		{
			if (chunk.m_containerMeshBlocks.getMeshBlock (
				ref chunk.m_containerMeshBlocksIteratorItem, 
				blockID, 
				ref m_currentMeshBlock)) {

				UnityEngine.Vector3 pos = new UnityEngine.Vector3 (0, 0, 0);
				Face face = new Face ();
				face.setPosition (position); 
				face.setFace (typeFace);
				m_currentMeshBlockFace = typeFace;
				//m_currentPositionInChunk = position;
				m_chunk = chunk;
				m_oppositeBlockFace.m_positionInChunk = position;
				m_oppositeBlockFace.m_face = typeFace;
				m_oppositeBlockFace.createOppositeBlockArray ();
				m_oppositeBlockFace.createVertexPowerColor (ref face);


				ContainerFace newContainer3DFace = null;
				if (false) {
					//if (m_chunk.m_containerTypeFaceSwap [m_chunk.m_swapLOD].findContainerFace (
					//	ref m_chunk.m_containerTypeFaceIteratorItemSwap [m_chunk.m_swapLOD],
					//	typeFace, 
					//	false, 
					//	false, 
					//	ref m_currentMeshBlock, 
					//	ref m_findContainer3DFace)) {

					//	m_findContainer3DFace.fusion7 (ref face);
				} else {
					newContainer3DFace = new ContainerFace (
						(int)chunk.m_unitLenght, 
						typeFace, 
						false, 
						false, 
						ref m_currentMeshBlock, 
						chunk.m_unitLenght,
						chunk.m_position,
						m_world.m_diffuseFast,
						m_chunk.m_updatePlayerBlock,
						m_chunk.m_fusionFace
					);
					newContainer3DFace.fusion (ref face);
					newContainer3DFace.createMesh ();
					newContainer3DFace.createGameObject ();
					chunk.m_containerTypeFaceSwap [chunk.m_swapLOD].pushEnd (newContainer3DFace);
				}
			}

		}


		public void addFace (
			ref Chunk chunk,
			UnityEngine.Vector3 position,
			int blockID,
			int typeFace
		)
		{
			if (chunk.m_containerMeshBlocks.getMeshBlock (
				ref chunk.m_containerMeshBlocksIteratorItem, 
				blockID, 
				ref m_currentMeshBlock)) {

				UnityEngine.Vector3 pos = new UnityEngine.Vector3 (0, 0, 0);
				Face face = new Face ();
				face.setPosition (position); 
				face.setFace (typeFace);
				m_currentMeshBlockFace = typeFace;
				//m_currentPositionInChunk = position;
				//m_chunk = chunk;
				//m_oppositeBlockFace.m_positionInChunk = position;
				//m_oppositeBlockFace.m_face = typeFace;
				m_oppositeBlockFace.resetBlocks (
					typeFace, position);
				m_oppositeBlockFace.createOppositeBlockArray ();
				m_oppositeBlockFace.createVertexPowerColor (ref face);



				ContainerFace newContainer3DFace = null;
				if (false) {
					//if (m_chunk.m_containerTypeFaceSwap [m_chunk.m_swapLOD].findContainerFace (
					//	ref m_chunk.m_containerTypeFaceIteratorItemSwap [m_chunk.m_swapLOD],
					//	typeFace, 
					//	false, 
					//	false, 
					//	ref m_currentMeshBlock, 
					//	ref m_findContainer3DFace)) {

					//	m_findContainer3DFace.fusion7 (ref face);
				} else {
					newContainer3DFace = new ContainerFace (
						(int)chunk.m_unitLenght, 
						typeFace, 
						false, 
						false, 
						ref m_currentMeshBlock, 
						chunk.m_unitLenght,
						chunk.m_position,
						m_world.m_diffuseFast,
						chunk.m_updatePlayerBlock,
						m_chunk.m_fusionFace
					);
					newContainer3DFace.fusion (ref face);
					newContainer3DFace.createMesh ();
					newContainer3DFace.createGameObject ();
					chunk.m_containerTypeFaceSwap [chunk.m_swapLOD].pushEnd (newContainer3DFace);
				}
			}

		}

		public void deleteFace (
			ref Chunk chunk,
			UnityEngine.Vector3 position,
			int blockID,
			int typeFace

		)
		{   
			bool find = false;
			ContainerFace.IteratorItem containerFaceIteratorItem = null;
			if (chunk.m_containerMeshBlocks.getMeshBlock (
				ref chunk.m_containerMeshBlocksIteratorItem, 
				blockID, 
				ref m_currentMeshBlock)) {

				Face findFace = null;
				chunk.m_containerTypeFaceSwap [chunk.m_swapLOD].resetIterator (
					ref chunk.m_containerTypeFaceIteratorItemSwap [chunk.m_swapLOD]);
				while (!find && chunk.m_containerTypeFaceSwap [chunk.m_swapLOD].hasNext (
					ref chunk.m_containerTypeFaceIteratorItemSwap [chunk.m_swapLOD],
					ref m_findContainer3DFace)) {

					if (m_findContainer3DFace.m_face == typeFace
						&& m_findContainer3DFace.m_meshBlock.m_ID == blockID ) {
						m_findContainer3DFace.newIterator (ref containerFaceIteratorItem);
						if (m_findContainer3DFace.findFaceAtPosition (
							ref containerFaceIteratorItem, position, ref findFace)) {

							if (m_findContainer3DFace.getSize () > 1) {
								findFace.m_garbage = true;
								find = true;
								m_findContainer3DFace.updateMesh ();
								//Debug.Log ("jcccccccccc");
								m_findContainer3DFace.updateGameObject ();
							} else {
								find = true;
								chunk.m_totalFace -= m_findContainer3DFace.getSize ();
								m_findContainer3DFace.destroyUnityObjects ();
								chunk.m_containerTypeFaceSwap [chunk.m_swapLOD].popAtIterator (
									ref chunk.m_containerTypeFaceIteratorItemSwap [chunk.m_swapLOD],
									ref m_findContainer3DFace);
							}
						} else {

						}
					}
				}
			}
			if (!find)
				Debug.Log ("findface null");
		}




		public override void execute()
		{

			//int itContainerBlocksID = 0; 

			//int currentBlockID = new int ();
			//int currentOppositeBlockID = new int ();

			//bool currentIsBlend = false;
			//bool currentIsInWater = false;
			//int currentMeshBlockFace;
			//int meshBlockFaceIndex = 0;

			//UnityEngine.Vector3 currentPositionInWorld;
			//UnityEngine.Vector3 currentOppositePositionInWorld;
			//m_chunk.m_isFaceJobCall = false;

			//ContainerFace newContainer3DFace = null;
			//bool oppositePositionInChunk;
			//int blabla = 0;
			//Face currentFace = null;
			//bool fusionFace = false;

			MeshBlock2 newMeshBlock = null;
			Block2 oldBlock = null;
			Block2 currentOppositeBlock = null;
			//bool currentDirectSun;
			Face face = new Face();
			bool newBlockIsBlend = true;
			LightBlock2 itLightBlock = null;
			LightObject deletedLightObject = null;

			if (m_world.m_containerMeshBlocks.getMeshBlock(
				ref m_containerMeshBlockIteratorItem,
				m_blockID,
				ref m_currentMeshBlock)) {
				if (!m_currentMeshBlock.m_isBlend)
					newBlockIsBlend = false;
			}






			//m_chunk.getBlockIDPosInChunk(m_currentPositionInChunk, ref currentBlockID);
			if (m_chunk.getBlock2PosInChunk (m_position, ref oldBlock)) {
				// enlever encien block

				// setBlockId

				if (oldBlock.m_ID != 0 && m_blockID == 0) {

					m_world.m_containerLightBlock2.getHead (ref itLightBlock);
					if (!object.ReferenceEquals (itLightBlock, null)) {
						oldBlock.createLightning ();
						oldBlock.addLightBlock (ref itLightBlock);
						oldBlock.getFirstLightBlock (ref itLightBlock);
						itLightBlock.m_directLight = true;
						itLightBlock.m_ambientPower = 0.0f;
					}
				}

				for (int f = 0; f < 6; f++) {
					face.setFace ( f + 1);
					face.setPosition (m_chunk.getPosInChunkToWorldPos (m_position));

					currentOppositeBlock = null;
					m_chunk.getOppositeBlock2 (ref m_currentOppositeChunk, 
						ref currentOppositeBlock, m_position, f);

					if (!object.ReferenceEquals (currentOppositeBlock, null)) {
						if (currentOppositeBlock.m_ID == 0) {

							if (oldBlock.m_ID != 0)
								// enlever encienne face (oldblock)
							if (f > -1 ) {
								deleteFace (ref m_chunk, m_chunk.getPosInChunkToWorldPos (m_position),
									oldBlock.m_ID, f + 1);
								//Debug.Log ("deleteFace" + m_chunk.getPosInChunkToWorldPos (m_position).ToString ());
							}
							//ajouter nouvelle face (newblock)
							if (m_blockID != 0) {
								if (m_blockID != 0)
									addFace (ref m_chunk, m_chunk.getPosInChunkToWorldPos (m_position),
										m_blockID, f + 1);
							}
						} else {
							// oppposite blockid != 0
							if (oldBlock.m_ID != 0) {
								//enlever encienee face (oldblock)
								//deleteFace (ref m_chunk, m_chunk.getPosInChunkToWorldPos (m_position),
								//oldBlock.m_ID, face.getOppositeFace());
								if (m_blockID == 0) {
									addFace (ref m_currentOppositeChunk, face.getOppositePosition (),
										currentOppositeBlock.m_ID,
										face.getOppositeFace ());
									//ajouter nouvelle face (oppositblock)
								} else {
									//face caches								}
								}
							}
							else { 
								// oldblockid == 0
								if (m_blockID != 0 && !newBlockIsBlend) {
									//delete  face (oppositeblock)
									deleteFace (ref m_currentOppositeChunk, face.getOppositePosition (),
										currentOppositeBlock.m_ID, face.getOppositeFace());
								}
								//rien faire avait et aura faces caches
							}
						}
					} else {
						//opposite block null

						if (oldBlock.m_ID != 0)
							// enlever encienne face (oldblock)
							deleteFace (ref m_chunk, m_chunk.getPosInChunkToWorldPos (m_position),
								oldBlock.m_ID, face.getOppositeFace());
						//ajouter nouvelle face (newblock)
						if (m_blockID != 0) {
							addFace (ref m_chunk, m_chunk.getPosInChunkToWorldPos (m_position),
								m_blockID, f + 1);
						}
					}

				}

			}
			if (m_blockID == 15 && oldBlock.m_ID != 15) {
				m_chunk.m_containerLightObject.addLightObject(m_chunk.getPosInChunkToWorldPos(m_position), true);
			}

			
			if (oldBlock.m_ID == 15 && m_blockID != 15) {
				if (m_chunk.m_containerLightObject.deleteLightObject (
					ref m_chunk.m_containerLightObjectIteratorItem,
					m_chunk.getPosInChunkToWorldPos (m_position),
					ref deletedLightObject)) {
					if (ReferenceEquals (m_world.m_currentLightObject, deletedLightObject)) {
						m_world.m_currentLightObject = null;
					}

				}
			}

			m_chunk.setBlockIDPosInChunk (m_position, m_blockID);
			m_chunk.m_containerPlayerBlocks.setBlockID (
				ref m_chunk.m_playerBlocksIteratorItem, 
				m_position, 
				m_blockID);




			m_isTerminated = true;
			//Debug.Log ("contFPS" + m_countFPS.ToString());
			//Debug.Log ("end");

		}


		public void addBlockNoLight(ref Chunk chunk, Vector3 m_position, int m_blockID)
		{

			Chunk currentOppositeChunk = null;
			LightObject deletedLightObject = null;

			MeshBlock2 newMeshBlock = null;
			//Block oldBlock = null;
			int oldBlockID = 0;
			int currentOppositeBlockID = 0;
			//Block currentOppositeBlock = null;
			//bool currentDirectSun;
			Face face = new Face();
			bool newBlockIsBlend = true;
			//LightBlock itLightBlock = null;

			if (m_world.m_containerMeshBlocks.getMeshBlock(
				ref m_containerMeshBlockIteratorItem,
				m_blockID,
				ref m_currentMeshBlock)) {
				if (!m_currentMeshBlock.m_isBlend)
					newBlockIsBlend = false;
			}






			//m_chunk.getBlockIDPosInChunk(m_currentPositionInChunk, ref currentBlockID);
			if (chunk.getBlockIDPosInChunk (m_position, ref oldBlockID)) {
				// enlever encien block

				// setBlockId

				if (oldBlockID != 0 && m_blockID == 0) {
					/**
					m_world.m_containerLightBlock.getHead (ref itLightBlock);
					if (!object.ReferenceEquals (itLightBlock, null)) {
						oldBlock.addLightBlock (ref itLightBlock);
						oldBlock.getFirstLightBlock (ref itLightBlock);
						itLightBlock.m_directLight = true;
						itLightBlock.m_ambientPower = 0.0f;
					}
					**/
				}

				for (int f = 0; f < 6; f++) {
					face.setFace ( f + 1);
					face.setPosition (chunk.getPosInChunkToWorldPos (m_position));

					currentOppositeBlockID = -1;
					if (chunk.getOppositeBlockID (ref currentOppositeChunk, 
						ref currentOppositeBlockID, m_position, f)
					) {


						if (currentOppositeBlockID == 0) {

							if (oldBlockID != 0)
								// enlever encienne face (oldblock)
							if (f > -1 ) {
								deleteFace (ref chunk, chunk.getPosInChunkToWorldPos (m_position),
									oldBlockID, f + 1);
								//Debug.Log ("deleteFace" + m_chunk.getPosInChunkToWorldPos (m_position).ToString ());
							}
							//ajouter nouvelle face (newblock)
							if (m_blockID != 0) {
								if (m_blockID != 0)
									addFace (ref chunk, chunk.getPosInChunkToWorldPos (m_position),
										m_blockID, f + 1);
							}
						} else {
							// oppposite blockid != 0
							if (oldBlockID != 0) {
								//enlever encienee face (oldblock)
								//deleteFace (ref m_chunk, m_chunk.getPosInChunkToWorldPos (m_position),
								//oldBlock.m_ID, face.getOppositeFace());
								if (m_blockID == 0) {
									addFace (ref currentOppositeChunk, face.getOppositePosition (),
										currentOppositeBlockID,
										face.getOppositeFace ());
									//ajouter nouvelle face (oppositblock)
								} else {
									//face caches								}
								}
							}
							else { 
								// oldblockid == 0
								if (m_blockID != 0 && !newBlockIsBlend) {
									//delete  face (oppositeblock)
									deleteFace (ref currentOppositeChunk, face.getOppositePosition (),
										currentOppositeBlockID, face.getOppositeFace());
								}
								//rien faire avait et aura faces caches
							}
						}
					} else {
						//opposite blockid null

						if (oldBlockID != 0)
							// enlever encienne face (oldblock)
							deleteFace (ref chunk, chunk.getPosInChunkToWorldPos (m_position),
								oldBlockID, face.getOppositeFace());
						//ajouter nouvelle face (newblock)
						if (m_blockID != 0) {
							addFace (ref chunk, chunk.getPosInChunkToWorldPos (m_position),
								m_blockID, f + 1);
						}
					}
					//debugLog ();
				} // for 6 face

			}

			chunk.setBlockIDPosInChunk (m_position, m_blockID);
			chunk.m_containerPlayerBlocks.setBlockID (
				ref chunk.m_playerBlocksIteratorItem, 
				m_position, 
				m_blockID);
			if (m_blockID == 14 && oldBlockID != 14) {
				chunk.m_containerLightObject.addLightObject(m_chunk.getPosInChunkToWorldPos(m_position), true);
			}

			if (oldBlockID == 14) {
				if (chunk.m_containerLightObject.deleteLightObject (
					ref chunk.m_containerLightObjectIteratorItem,
					chunk.getPosInChunkToWorldPos (m_position),
					ref deletedLightObject)) {
					if (ReferenceEquals (m_world.m_currentLightObject, deletedLightObject)) {
						m_world.m_currentLightObject = null;
					}

				}
			}


			m_isTerminated = true;
			//Debug.Log ("contFPS" + m_countFPS.ToString());
			//Debug.Log ("end");

		}






		public override bool checkFlags1 (bool debugLog, ref int state) {

			bool allFlags = false;
				allFlags = true;
			m_setFlag = true;

			if (debugLog)
				Debug.Log ("Flags face empty block :" 
					+ allFlags.ToString ()
					+ " " + m_chunk.m_position.ToString()
				);
			return allFlags;
		}


		public override bool checkFlags2 (bool debugLog, ref int state) {

			bool allFlags = false;
			if (m_chunk.checkFaceJobTerminated (debugLog)
				&& m_chunk.checkOppositeFaceJobTerminated (debugLog)) {
				allFlags = true;
			} 
			if (debugLog)
				Debug.Log ("Flags face :" 
					+ allFlags.ToString ()
					+ " " + m_chunk.m_position.ToString()
				);
			return allFlags;
		}

		public override void setFlags(bool debugLog) {
			
			m_setFlag = true;

		}

		public override bool checkTerminatedFlag(bool debugLog) { 
			//debugLog = true;
			//if (m_isTerminated)
			//	Debug.Log ("tttttttttttttttt");
			if (debugLog) {
				if (m_isTerminated)
					Debug.Log ("Flags face " + m_chunk.m_position.ToString () + "terminated");
				else
					Debug.Log ("Flags face " + m_chunk.m_position.ToString () + "not terminated");
			}
			return m_isTerminated;
		}

	} // class MakeFaceJob





	
	public class ContainerAddBlocksJobs : ContainerJobs2
	{
		

		private MakeFaceJob2 m_itMakeFaceJob;


		public ContainerAddBlocksJobs(
						World world,
			ref bool isThread,
			int orbitalIndex
		) : base (ref world, ref isThread, orbitalIndex)
		{
			
		}



			public void addJob(ref Chunk chunk, Vector3 positionInWorld, int blockID)
		{

			m_total++;
				AddBlocksJob newJob = new AddBlocksJob(
				ref m_world,
				ref chunk
			);
			newJob.m_position = positionInWorld;
			newJob.m_blockID = blockID;
			pushEnd(newJob);
		}

	

	

	}
	
}


