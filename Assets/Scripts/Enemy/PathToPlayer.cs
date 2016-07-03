using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class PathToPlayer : MonoBehaviour {

    private static char [,]walls;
    private static List <Vector2> badPos;
    private static PrintMap map;

    public const int WORLD_SIZE = 70;

    public class Vector2i
    {
        public Vector2i(int _x, int _y) {
            x = _x;
            y = _y;
        }
	    public Vector2i(){}
	    public int x, y;
    }
    public class SearchCell
    {


	    public SearchCell(){

        }
	    public SearchCell(int x, int y, SearchCell scPtr_parent){
            m_xcoord = x; m_ycoord = y;
            scPtrParent = new SearchCell();
            scPtrParent = scPtr_parent;
            m_id = y * WORLD_SIZE + x;
            //Cambiado el + y por + x;
            G = 0; H = 0;
        }
        ~SearchCell(){}

	    public int GetF(){ return G + H; }
	    public int ManHattanDis(SearchCell scPtr_nodeEnd)
	    {
            int d = Mathf.Abs(this.m_xcoord - scPtr_nodeEnd.m_xcoord) +
                    Mathf.Abs(this.m_ycoord - scPtr_nodeEnd.m_ycoord);
		    return d;
	    }

	    public int m_xcoord, m_ycoord;
	    public int m_id;
	    public SearchCell scPtrParent;//pointer
	    public int G;
	    public int H;

    };

    public class PathFinding
    {

        public PathFinding()
        {
            m_initializedStartGoal = false;
            m_foundGoal = false;
            scPtr_openList = new List<SearchCell>();
            scPtr_visitedList = new List<SearchCell>();
            v2_pathToGoal = new List<Vector2i>();

            map = GameObject.Find("Main Camera").GetComponent<PrintMap>();
            badPos = map.badPositions;
            walls = new char [100, 100] ;
            foreach(Vector2 bad in badPos){
              walls [(int)bad.x,(int)bad.y] = 'p';
            }
        }
	    ~PathFinding(){}

        public void FindPath(Vector2i currentPos, Vector2i targetPos)
        {
            if (!m_initializedStartGoal)
            {

                ClearOpenList();
                ClearVisitedList();
                ClearPathtoGoal();

                //Initialize Start;
                SearchCell start = new SearchCell();
                start.m_xcoord = currentPos.x;
                start.m_ycoord = currentPos.y;

                //Initialize Goal;
                SearchCell goal = new SearchCell();
                goal.m_xcoord = targetPos.x;
                goal.m_ycoord = targetPos.y;

                SetStartAndGoal(start, goal);
                m_initializedStartGoal = true;
            }

            if (m_initializedStartGoal)
            {
                ContinuePath();
            }
        }
        public Vector2i NextPathPos()
        {
            int index = 1;

            Vector2i nextPos = new Vector2i();
            if (v2_pathToGoal.Count > 0) {
                nextPos.x = v2_pathToGoal[v2_pathToGoal.Count - index].x;
                nextPos.y = v2_pathToGoal[v2_pathToGoal.Count - index].y;
            }
            /*Vector2i distance = nextPos - pos;

            if (index < v2_pathToGoal.size()){
            if (distance.)
            }*/


            return nextPos;
        }
	    public void ClearOpenList(){
            scPtr_openList.Clear();
        }
	    public void ClearVisitedList(){

		    scPtr_visitedList.Clear();

        }
        public void ClearPathtoGoal()
        {
            if(v2_pathToGoal.Count > 0)
                v2_pathToGoal.Clear();

        }

        public bool m_initializedStartGoal;
	    public bool m_foundGoal;

	    public List<Vector2i> v2_pathToGoal;


        private void SetStartAndGoal(SearchCell start, SearchCell goal)
        {
            SearchCell empty = new SearchCell();
            empty.m_xcoord = -9999;
            scPtr_startCell = new SearchCell(start.m_xcoord, start.m_ycoord, empty);
            scPtr_goalCell = new SearchCell(goal.m_xcoord, goal.m_ycoord, goal);

            scPtr_startCell.G = 0;
            scPtr_startCell.H = scPtr_startCell.ManHattanDis(scPtr_goalCell);
            //m_startCell.parent = nullptr;

            scPtr_openList.Add(scPtr_startCell);
        }
        private void PathOpened(int x, int y, int newCost, SearchCell parent)
        {
            //for (auto& pos : badPositions){
            //    if (pos.x == x && pos.y == y){
            //        return;
            //    }
            //}

            /*for (int i = 0; i < GameObject.Find("Main Camera").GetComponent<PrintMap>().badPositions.Count; i++)
            {
                if ((int)GameObject.Find("Main Camera").GetComponent<PrintMap>().badPositions[i].x == x &&
                    (int)GameObject.Find("Main Camera").GetComponent<PrintMap>().badPositions[i].y == y)
                {
                    return;
                }
            }*/
            if ((walls[x,y] == 'p')){return;};


	        int id = y * WORLD_SIZE + x;

	        for (int i = 0; i < scPtr_visitedList.Count; i++){

		        if (id == scPtr_visitedList[i].m_id){
			        return;
		        }

	        }

	        SearchCell newChild = new SearchCell(x, y, parent);
	        newChild.G = newCost;
	        newChild.H = parent.ManHattanDis(scPtr_goalCell);

	        for (int i = 0; i < scPtr_openList.Count; i++){
                if (id == scPtr_openList[i].m_id)
                {

			        int newF = newChild.G + newCost + scPtr_openList[i].H;

			        if (scPtr_openList[i].GetF() > newF)
			        {
				        scPtr_openList[i].G = newChild.G + newCost;
				        scPtr_openList[i].scPtrParent = newChild;
			        }
			        else //if the new F is not better
			        {
				        return;
			        }
		        }
	        }

	        scPtr_openList.Add(newChild);
        }
        private SearchCell GetNextCell()
        {
            int bestF = 100000;
	        int cellIndex = -1;
	        SearchCell nextCell = new SearchCell();

	        for (int i = 0; i < scPtr_openList.Count; i++){
                if (scPtr_openList[i].GetF() < bestF)
                {
                    bestF = scPtr_openList[i].GetF();
			        cellIndex = i;
		        }
	        }
	        if (cellIndex >= 0) {
		        nextCell = scPtr_openList[cellIndex];
                scPtr_visitedList.Add(nextCell);
                scPtr_openList.RemoveAt(cellIndex);
            }

	        return nextCell;
        }
        private void ContinuePath()
        {
            if (scPtr_openList.Count == 0){
		        return;
	        }

	        SearchCell currentCell = GetNextCell();
	        if (currentCell.m_id == scPtr_goalCell.m_id)
	        {
		        scPtr_goalCell.scPtrParent = currentCell.scPtrParent;

		        SearchCell getPath = new SearchCell();

		        for (getPath = scPtr_goalCell; getPath.m_xcoord != -9999; getPath = getPath.scPtrParent){
			        this.v2_pathToGoal.Add(new Vector2i(getPath.m_xcoord, getPath.m_ycoord));
		        }
		        m_foundGoal = true;

	        }
	        else{
		        //rightSide
		        PathOpened(currentCell.m_xcoord + 1, currentCell.m_ycoord, currentCell.G + 1, currentCell);
		        //leftSide
		        PathOpened(currentCell.m_xcoord - 1, currentCell.m_ycoord, currentCell.G + 1, currentCell);
		        //Up
		        PathOpened(currentCell.m_xcoord, currentCell.m_ycoord - 1, currentCell.G + 1, currentCell);
		        //Down
		        PathOpened(currentCell.m_xcoord, currentCell.m_ycoord + 1, currentCell.G + 1, currentCell);
		        /* Diagonals */

		        for (int i = 0; i < scPtr_openList.Count; i++){
			        if (currentCell.m_id == scPtr_openList[i].m_id){
				        scPtr_openList.RemoveAt(i);
			        }
		        }
	        }
        }
	    private SearchCell scPtr_startCell;
	    private SearchCell scPtr_goalCell;
	    private List<SearchCell> scPtr_openList;
	    private List<SearchCell> scPtr_visitedList;
    };

    // Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public List<Vector2> GetPathToPlayer(Vector2 currentPosition){

        Vector2i currentP = new Vector2i((int)currentPosition.x, (int)currentPosition.y);

        List<Vector2> empty = new List<Vector2>();

        PathFinding _ptp = new PathFinding();
        _ptp.m_initializedStartGoal = false;
        _ptp.m_foundGoal = false;

        Vector2i playerPos = new Vector2i((int)GetComponent<PrintMap>().ply_.GetComponent<PlayerControl>().GetPosition().x,
                                          (int)GetComponent<PrintMap>().ply_.GetComponent<PlayerControl>().GetPosition().y);

        int maxtries = 0;
        while (!_ptp.m_foundGoal)
        {
            _ptp.FindPath(currentP, playerPos);
            if (maxtries++ > 300)
                return empty;
        }

        int lastIndex = _ptp.v2_pathToGoal.Count - 1;



        for (int i = lastIndex; i >= 0; --i)
        {
            Vector2 aux = new Vector2();
            aux.x = _ptp.v2_pathToGoal[i].x;
            aux.y = _ptp.v2_pathToGoal[i].y;
            empty.Add(aux);
        }


        return empty;
	}

}
