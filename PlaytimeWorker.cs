using PrisonersRiddle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

public class PlaytimeWorker
{
    static public int Playtime(int playtimeIterations, int numPlayers, int numAttemptsAllowed, Random randomizer)
    {
        int cntSuccess = 0;
        for (int idx = 0; idx < playtimeIterations; idx++)
        {
            List<int> boxes = GetBoxList(numPlayers);
            RandomizeBoxes(boxes, randomizer);
            if (GroupsPlaytime(numPlayers, numAttemptsAllowed, boxes)) cntSuccess++;
        }
        return cntSuccess;
    }

    static private bool GroupsPlaytime(int numPlayers, int numAttemptsAllowed, List<int> boxes)
    {
        int cntPlayerFoundTheirNumber = 0;
        for (int playerNumber = 0; playerNumber < numPlayers; playerNumber++)
        {
            if (PlayerFindYourNumber(playerNumber, numAttemptsAllowed, boxes)) cntPlayerFoundTheirNumber++;
        }
        // $"Number of players: {numPlayers} found their box {cntPlayerFoundTheirNumber} times".Dump(); 
        if (cntPlayerFoundTheirNumber == numPlayers)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    static private bool PlayerFindYourNumber(int playerNumber, int numAttemptsAllowed, List<int> boxes)
    {
        int currentBoxAttempt = playerNumber;
        while (numAttemptsAllowed > 0)
        {
            if (boxes[currentBoxAttempt] == playerNumber)
            {
                return true;
            }
            currentBoxAttempt = boxes[currentBoxAttempt];
            numAttemptsAllowed--;
        }
        return false;
    }




    static private List<int> GetPlayers(int cnt)
    {
        // Enumerable is oddly sdlightly slower than a for loop. 
        return new List<int>(Enumerable.Range(0, cnt));
    }


    static private List<int> GetBoxList(int cnt)
    {
        // for loop is oddly slightly faster than using Enumerable 
        List<int> l = new List<int>();
        for (int idx = 0; idx < cnt; idx++)
        {
            l.Add(idx);
        }
        return l;
    }


    static private void RandomizeBoxes(List<int> boxes, Random r)
    {
        // in .Net 8 explore random Random.shuffle 
        boxes.Shuffle(r);

    }



}
