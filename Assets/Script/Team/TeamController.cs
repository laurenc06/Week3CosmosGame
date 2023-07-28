using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeamController : MonoBehaviour
{
    //Resources
    public float wood;
    public float gold;
    public int team;
    public int unitCap;
    public int unitNum;
    public int workerNum;
    public int campNum;
    public int barracksNum;
    public int towerNum;
    public int archerNum;
    public int warriorNum;
    public GameObject prefabBarracks;
    public GameObject prefabCamp;
    public Vector3 spawnLocation;
    public bool createCamp;
    public int teamNumber;
    StateController controller;
    public int waveNumber;
    System.Random rnd;
    public int randomNum;
    public float actionRate = 10f;
    public float lastAction = 0f;

    //Probably should have it's own FSM (state controller)

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        unitCap = 5;
        createCamp = false;
        workerNum = 0;
        campNum = 0;
        barracksNum = 0;
        towerNum = 0;
        archerNum = 0;
        warriorNum = 0;
        waveNumber = 0;
        wood = 25;
        gold = 25;
    }

    // Update is called once per frame
    void Update()
    {
        lastAction += Time.deltaTime;
        rnd = new System.Random();
        randomNum = rnd.Next(0, 24);
    
        if(lastAction >= actionRate){
            switch (waveNumber)
                {
                    case 0:
                        if(/*remember to chec kfor adeequate number of resources*/workerNum < 2 && gold >= 5 && this.GetComponent<Base>().lastSpawn > this.GetComponent<Base>().spawnRate){
                            this.GetComponent<Base>().createWorker = true;
                            gold -= 5;
                            lastAction = 0;
                        } else {
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildBarracks = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                            waveNumber = 1;
                        }
                        break;
                    case 1:
                        Debug.Log("Wave 1");
                        if(workerNum < 4 && randomNum < 4 && gold >= 5 && unitNum < unitCap){
                            this.GetComponent<Base>().createWorker = true;
                            gold -= 5;
                            lastAction = 0;
                        } else if (wood >= 25 && barracksNum == 0 && randomNum >= 4 && randomNum < 8){
                            //IDK why but sometimes this runs but doesnt build barracks
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildBarracks = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        } else if (wood >= 25 && campNum == 0 && randomNum >= 8 && randomNum < 12){
                            //Same with this
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildCamp = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        } else if (warriorNum < 2 &&  randomNum >= 12 && randomNum < 16 && unitNum < unitCap && gold > 5){
                            float numSoldiers = (int) Math.Round(gold / 5);
                            GameObject[] barracks = GameObject.FindGameObjectsWithTag("Barracks");
                            for(int count = 0; count < barracks.Length; count++){
                                if(count < numSoldiers && count < 5){
                                    barracks[count].GetComponent<Barracks>().createWarrior = true;
                                    gold -= 5;
                                }
                            }
                            lastAction = 0;
                            //create warrior at barracks
                            //Should take longer depending on how many barracks
                        } else if (archerNum < 2 &&  randomNum >= 16 && randomNum < 20 && unitNum < unitCap && gold > 5){
                            float numArchers = (int) Math.Round(gold / 5);
                            GameObject[] barracks = GameObject.FindGameObjectsWithTag("Barracks");
                            for(int count = 0; count < barracks.Length; count++){
                                if(count < numArchers && count < 5){
                                    barracks[count].GetComponent<Barracks>().createArcher = true;
                                    gold -= 5;
                                }
                            }
                            lastAction = 0;
                        }  else if (randomNum >= 20 && randomNum < 24 && towerNum == 0 && wood >= 25){
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildTower = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        } else if(workerNum >= 4 && barracksNum != 0 && campNum != 0 && warriorNum >= 2 && archerNum >= 2 && towerNum != 0){
                            waveNumber++;
                            lastAction = 0;
                        } 
                        break;
                    case 2:
                        Debug.Log("Wave 2");
                        if(workerNum < 6 && randomNum < 3 && gold >= 5 && unitNum < unitCap){
                            this.GetComponent<Base>().createWorker = true;
                            gold -= 5;
                            lastAction = 0;
                        } else if (wood >= 25 && barracksNum < 2 && randomNum >= 3 && randomNum < 6){
                            //IDK why but sometimes this runs but doesnt build barracks
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildBarracks = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        } else if (wood >= 25 && campNum < 4 && randomNum >= 6 && randomNum < 9){
                            //Same with this
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildCamp = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        } else if (warriorNum < 4 &&  randomNum >= 9 && randomNum < 12 && unitNum < unitCap){
                            float numSoldiers = (int) Math.Round(gold / 5);
                            GameObject[] barracks = GameObject.FindGameObjectsWithTag("Barracks");
                            for(int count = 0; count < barracks.Length; count++){
                                if(count < numSoldiers && count < 5){
                                    barracks[count].GetComponent<Barracks>().createWarrior = true;
                                    gold -= 5;
                                }
                            }
                            lastAction = 0;
                        } else if (archerNum < 4 &&  randomNum >= 12 && randomNum < 15 && unitNum < unitCap){
                            float numArchers = (int) Math.Round(gold / 5);
                            GameObject[] barracks = GameObject.FindGameObjectsWithTag("Barracks");
                            for(int count = 0; count < barracks.Length; count++){
                                if(count < numArchers && count < 5){
                                    barracks[count].GetComponent<Barracks>().createArcher = true;
                                    gold -= 5;
                                }
                            }
                            lastAction = 0;
                        } else if (randomNum >= 15 && randomNum < 18 && towerNum < 2 && wood >= 25){
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildTower = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        }else if (randomNum >= 18 && randomNum <= 20 && (warriorNum + archerNum) > 5){
                            Debug.Log("ATTACK!!!!");
                            GameObject[] archers = GameObject.FindGameObjectsWithTag("Archer");
                            GameObject[] warriors = GameObject.FindGameObjectsWithTag("Warrior");
                            for(int count = 0; count < archers.Length; count++){
                                if(archers[count].GetComponent<Archer>().teamNumber == teamNumber){
                                    archers[count].GetComponent<Archer>().controller.ChangeState(new ArcherInvade());
                                }
                            }
                            for(int count = 0; count < warriors.Length; count++){
                                if(warriors[count].GetComponent<Warrior>().teamNumber == teamNumber){
                                    warriors[count].GetComponent<Warrior>().controller.ChangeState(new WarriorInvade());
                                }
                            }

                        }else if(workerNum >= 6 && barracksNum == 3 && campNum == 4 & warriorNum >= 4 && archerNum >= 4 && towerNum >= 2){
                            waveNumber++;
                            lastAction = 0;
                        } 
                        break;
                    case 3:
                        Debug.Log("Wave 3");
                        if(workerNum < 10 && randomNum < 4 && gold >= 5 && unitNum < unitCap){
                            this.GetComponent<Base>().createWorker = true;
                            gold -= 5;
                            lastAction = 0;
                        } else if (wood >= 25 && barracksNum < 5 && randomNum >= 4 && randomNum < 8){
                            //IDK why but sometimes this runs but doesnt build barracks
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildBarracks = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        } else if (wood >= 25 && campNum < 5 && randomNum >= 8 && randomNum < 12){
                            //Same with this
                            GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");
                            for (int count = 0; count < workers.Length; count++){
                                if(workers[count] != null && workers[count].GetComponent<WorkerScript>().teamNumber == teamNumber){
                                //maybe renadomize which worker gets accessed;
                                    workers[count].GetComponent<WorkerScript>().buildCamp = true;
                                    lastAction = 0;
                                    wood -= 25;
                                    break;
                                }
                            }
                        } else if (warriorNum < 15 &&  randomNum >= 12 && randomNum < 16 && unitNum < unitCap){
                            lastAction = 0;
                            //create warrior at barracks
                            //Should take longer depending on how many barracks
                        } else if (archerNum < 15 &&  randomNum >= 16 && randomNum < 20 && unitNum < unitCap){
                            lastAction = 0;
                            //create archer at barracks
                            //Should take longer depending on how many barracks
                        } else if(workerNum >= 10 && barracksNum == 5 && campNum == 5 & warriorNum >= 15 && archerNum >= 15){
                            waveNumber++;
                            lastAction = 0;
                        } else if (randomNum >= 20 && randomNum < 24){
                            //randomly attack or scout???
                            lastAction = 0;
                        }
                        break;
                    default:
                        break;
                }
        }
    }

    public void IncreaseUnitCap(int increase){
        unitCap += increase;
    }

    public void IncreaseUnitNum(int increase){
        unitNum += increase;
    }

    public int GetUnitCap(){
        return unitCap;
    }

    public int GetUnitNum(){
        return unitNum;
    }
}
