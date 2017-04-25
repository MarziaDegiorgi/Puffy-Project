#include <SoftwareSerial.h>
#include <Time.h>

//Drive Commands
 byte Rstop[6]={137,0,0,0,0}; //stopping the drive motors is just a "zero" speed selection
 byte Rright[6]={137,0,100,0,1}; //plus 1 radius means turn in place - right
 byte Rleft[6]={137,0,100,255,255};  //-1 radius means turn in place - left
 //byte Rfwd[6]={137,0,100,0x80,0}  ;    //radius hex 8000 is straight
 byte Rback[6]= {137,255,-100,0x80,0}; 
 byte RCircleL[6]={137,0,100,0,128}; //Circle Left
 byte RCircleR[6]= {137,0,100,-1,-128}; //Circle Right

 byte RoombaMotorsOff[2] = {138,0};  //motors off code (brushes, vac)
 byte RoombaOff = 133;
 byte zero = 0;
 int CMD;
 unsigned long timeSpent;
 long timeAsk;

 enum State : int {
   StateStop,
   StateGo,
   StateTurnR,
   StateTurnL,
   StateBack,
   LightChange,
 };
 int actualState = StateStop ;

SoftwareSerial Roomba(10, 11); 

// Initialization Code ============================================
void setup()  
{
  // Open serial communications and wait for port to open:
  Serial.begin(57600); 
  Roomba.begin(57600); 
  
}

// Main Loop ======================================================
void loop() // run over and over
{
  if (Roomba.available()){   //First order of business: listen to Roomba
    Serial.write(Roomba.read());   //writes to USB input from soft serial
  }
  
  if (Serial.available()){  //check for command from computer USB
    CMD = Serial.read();
    timeAsk = 5;
    timeSpent= millis();
    String cmd = Serial.readString();
    //timeAsk = cmd.toInt(); 
//    if (timeAsk == 0){
//      Stop();   
//    }
//    else{
      StateSwitch(CMD);
    //}
  }
  else if ((millis()- timeSpent)>= (1000*timeAsk)){
    Stop();
  }
  else{
    StateSwitch(actualState);
  }
}

void StateSwitch(int CMD){
  Roomba.write(128);  //start (goes to Passive)
  delay(20);  //delay 20 milliseconds after state change
  Roomba.write(130);  //Command mode - goes to safe
  delay(20);  //delay 20 milliseconds after state change
  
  switch (CMD) {
    case StateGo : 
      Go(3,250);
      break;
    case StateStop :
      Stop();
      break;
    case StateTurnR :
      TurnRight();
      break;
    case StateTurnL :
      TurnLeft();
      break;
    case StateBack :
      Back(2);
      break;
    case LightChange :
      
      break;
    default :
      Serial.println("Arduino pb : receive wrong cmd"); 
  }  
}

// ============= Useful Control Subroutines ============
void TurnRight()
{
 actualState = StateTurnR;
 timeSpent = millis();  
 Roomba.write(Rright,6);
 Serial.println("Turn right");
}

void TurnLeft()
{
 actualState = StateTurnL;
 timeSpent = millis();  
 Roomba.write(Rleft,6);
 Serial.println("Turn Left");
}

void Go(int seconds, int spd)  //Go foreward for some number of seconds
{
 byte Rfwd[6]={137,0,spd,0x80,0};
 actualState = StateGo;
 timeSpent = millis();  
 Roomba.write(Rfwd,6);
 Serial.println("Go");
}

void Back(int seconds)
{
 actualState = StateBack;
 timeSpent = millis();  
 Roomba.write(Rback,6);
 Serial.println("Back");
}

void Stop()
{
  actualState = StateStop;
  Roomba.write(Rstop,6);
  Roomba.write(RoombaMotorsOff,2);
  Serial.println("Stop");
}
