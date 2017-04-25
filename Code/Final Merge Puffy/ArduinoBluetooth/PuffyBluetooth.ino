#include <SoftwareSerial.h>
#include <Time.h>

//Drive Commands
 byte Rstop[6]={137,0,0,0,0}; //stopping the drive motors is just a "zero" speed selection
 byte Rleft[6]={137,0,100,0,1}; //plus 1 radius means turn in place - right
 byte Rright[6]={137,0,100,255,255};  //-1 radius means turn in place - left
 //byte Rfwd[6]={137,0,100,0x80,0}  ;    //radius hex 8000 is straight
 byte Rback[6]= {137,250,-140,0x80,0}; 
 byte RCircleL[6]={137,0,180,0,128}; //Circle Left
 byte RCircleR[6]= {137,0,180,-1,-128}; //Circle Right

 byte RoombaMotorsOff[2] = {138,0};  //motors off code (brushes, vac)
 byte RoombaOff = 133;
 byte zero = 0;
 int CMD;
 unsigned long timeSpent;
 long timeAsk;

SoftwareSerial Roomba(10, 11);
SoftwareSerial Bl(2,3); 

// Initialization Code ============================================
void setup()  
{
  // Open serial communications and wait for port to open:
  Serial.begin(57600); 
  Roomba.begin(57600); 
  Bl.begin(9600);

  Roomba.write(128);  //start (goes to Passive)
  delay(20);  //delay 20 milliseconds after state change
  Roomba.write(130);  //Command mode - goes to safe
  delay(20);  //delay 20 milliseconds after state change
  Serial.println("Init ok");
}

// Main Loop ======================================================
void loop() // run over and over
{
  if (Roomba.available()){   //First order of business: listen to Roomba
    Serial.write(Roomba.read());   //writes to USB input from soft serial
  }
  
  if ( Bl.available()){  //check for command from computer USB
   
    CMD = Bl.read();
    timeAsk = 2000;
    StateSwitch(CMD);
    }
}

void StateSwitch(char CMD){
  switch (CMD) {
    case 's' :
      Start();
      break;
    case 'g' : 
      Go(3,250);
      break;
    case ' ' :
      Stop();
      break;
    case 'r' :
      TurnRight();
      break;
    case 'l' :
      TurnLeft();
      break;
    case 'b' :
      Back(2);
      break;
    default :
      Serial.println("Arduino pb : receive wrong cmd"); 
  } 
}

// ============= Useful Control Subroutines ============
void TurnRight()
{
 Roomba.write(Rright,6);
 Serial.println("Turn right");
 delay(timeAsk);
 Stop();
}

void TurnLeft()
{
 Roomba.write(Rleft,6);
 Serial.println("Turn Left");
 delay(timeAsk);
 Stop();
}

void Go(int seconds, int spd)  //Go foreward for some number of seconds
{
 byte Rfwd[6]={137,0,spd,0x80,0};
 Serial.println("Go");
 Roomba.write(Rfwd,6);
 delay(timeAsk);
 Stop();
}

void Back(int seconds)
{ 
 Roomba.write(Rback,6);
 Serial.println("Back");
 delay(timeAsk);
 Stop();
}

void Stop()
{
  Roomba.write(Rstop,6);
  Serial.println("Stop");
}

void Start()
{
  Roomba.write(128);  //start (goes to Passive)
  delay(20);  //delay 20 milliseconds after state change
  Roomba.write(130);  //Command mode - goes to safe
  delay(20);  //delay 20 milliseconds after state change
  Serial.println("Start");
}
