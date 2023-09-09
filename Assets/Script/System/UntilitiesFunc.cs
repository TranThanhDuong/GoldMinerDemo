using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UntilitiesFunc
{
    public static string ToMinusAndSec(this int time)
    {
        string text = "";

        float min = Mathf.Floor(time / 60);
        if(min > 0)
        {
            if(min >= 10)
            {
                text += min;
            }   
            else
            {
                text += "0" + min;
            }

            text += ":";

            int sec = time - (int)min * 60;
            if(sec >= 10)
            {
                text += min;
            }
            else
            {
                text += "0" + sec;
            }
        }    
        else
        {
            if (time >= 10)
            {
                text = "00:"+ time;
            }
            else
            {
                text += "00:0" + time;
            }
        }    

        return text;
    }
    public static string ToNumberSeparateByComma(this int score)
    {
        string text = "";
        float curMoney = score;
        while(true)
        {
            float greaterThousand = Mathf.Floor(curMoney / 1000f);
            if(greaterThousand > 0)
            {
                float lowerThousand = curMoney - greaterThousand * 1000f;
                if(lowerThousand >= 100)
                {
                    text = "," + lowerThousand + text;
                }   
                else if(lowerThousand >= 10)
                {
                    text = ",0" + lowerThousand + text;
                }
                else if(lowerThousand > 0)
                {
                    text = ",00" + lowerThousand + text;
                }
                else
                {
                    text = ",000" + text;
                }    
                curMoney = greaterThousand;
            }    
            else
            {
                text = curMoney + text;
                break;
            }    
        }    
        return text;
    }
}
