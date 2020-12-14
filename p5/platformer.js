new p5();
//Floor pixel limit: 560
var Floor = 560;
//-------------------------Variables/Setup---------------------------
var idleStart = 1;
var walkStart = 1;
var runStart = 1;
var jumpStart = 1;
var bg1 = loadImage("bg1.png");
var keyZ = loadImage("keyZ_48px.png");
var keySpace = loadImage("keySpace_48px.png");
var idle = [];
var idleUse;
var runUse;
var walkUse;
var jumpUse;
var animation;
var walk = [];
var run = [];
var jump =[];
var jumpLimit = 20;
var i = 1;
var w = 1;
var j = 1;
var r = 1;
var gravity = 0;
var scrolling = 0;
var onPlat, canJump = false;
var canJump = true;
var timer = 0;
var left, right, up, down, jumpu, dirL, running = false;
var dirR = true;
var under = false;


var Screen = {
  x: 720,
  y: 720
}

var setup = function(){
  frameRate(30);
  createCanvas(720,720)
};

var player = {
  xpos: 600,
  ypos: Floor,
  width: 100,
  height: 100,
  size: 100,
  speed: 10,
};


var boxes = [{xpos: 500, ypos: 500, width:200, height: 5}];


//-----------------------------Initial Loading---------------------------

//SPRITE IDLE ANIMATION ARRAY LOAD
while(idleStart < 17){
    idle[idleStart] = loadImage("Idle_(" + idleStart + ").png");
    idleStart++;
}

//SPRITE WALK ANIMATION ARRAY LOAD
while(walkStart < 21){
    walk[walkStart] = loadImage("Walk_(" + walkStart + ").png");
    walkStart++;
}

//SPRITE JUMP ANIMATION ARRAY LOAD
while(jumpStart < 31){
    jump[jumpStart] = loadImage("Jump_(" + jumpStart + ").png");
    jumpStart++;
}

//SPRITE RUN ANIMATION ARRAY LOAD
while(runStart < 21){
    run[runStart] = loadImage("Run_(" + runStart + ").png");
    runStart++;
}

//-----------------------------The Game---------------------------

var draw = function() {
  animation = idleUse;
  timer++;
  
  drawBG();
  UI();
  smooth();
  //noFill();
  strokeWeight(5);
  rect(boxes[0].xpos+(scrolling),boxes[0].ypos,boxes[0].width,boxes[0].height);
  
  drawPlayer(); 
  
  jumping();
  
  if(rectCollision(player,boxes[0]) && gravity > 0){
    textSize(24);
    text("touching platform", 900,100)
    
      //if player is under platform, knock player down
       
         player.ypos = boxes[0].ypos- 100;
       

        
      
      
      
    
    gravity = 0;
    onPlat = true;
  }
  else{
    onPlat = false;
  }
  
 playerAnimationController();
 

  
};


//-----------------------------Functions---------------------------

var keyPressed = function(){
  if(keyCode === RIGHT_ARROW || keyCode === 68){
    right = true;
    dirR = true;
    dirL = false;
  }
  if(keyCode === LEFT_ARROW || keyCode === 65){
    left = true;
    dirL = true;
    dirR = false;
  }
  if(keyCode === UP_ARROW || keyCode === 87){
    up = true;
  }
  if(keyCode === DOWN_ARROW || keyCode ===83){
    down = true;
  }
  //SpaceKey
  if(canJump){
    if(keyCode === 32 ){
      jumpu = true;
    }
  }
  //Z Key
  if(keyCode === 90 || keyCode === 16){
    running = true;
  }
};



var keyReleased = function(){
  if(keyCode === RIGHT_ARROW || keyCode === 68){
    right = false;
  }
  if(keyCode === LEFT_ARROW || keyCode === 65){
    left = false;
  }
  if(keyCode === UP_ARROW || keyCode === 87){
    up = false;
  }
  if(keyCode === DOWN_ARROW || keyCode ===83){
    down = false;
  }
  //SpaceKey
  if(keyCode === 32){
    jumpu = false;
  }
  //Z Key
  if(keyCode === 90 || keyCode === 16){
    running = false;
  }
};


var jumping = function(){
  
  if(jumpu){
    if(gravity > -jumpLimit){
    gravity -=10;   
    }
  }
  else if(player.ypos == Floor){
    gravity = 0;
  }
  else {
    if(gravity < 20){
      gravity +=5;
    } else {
      gravity += 1;
    }
  }

  if(!rectCollision(player,boxes[0])){
    player.ypos += gravity;
  }
  
  //Floor
  if (player.ypos > Floor){
    player.ypos = Floor;
  }
  
  //Ceiling
  if (player.ypos < 0){
    player.ypos = 0;
  }
 
  //Disables pressing jump in midair
  if (player.ypos === Floor){
    canJump = true;
  }
  else if (onPlat){
    canJump = true;
  }
  else {
    canJump = false;
  }
  
  //Maximum Jump Height
 
};

var UI = function(){
  textSize(24);
  image(keyZ, 30, 20, 48, 48);
  text("= Run", 90, 50);
  image(keySpace, 30, 80, 48, 48);
  text("= Jump", 90, 110);
  text("Gravity: " + gravity, 250,100);
  text("canJump: " + canJump, 500, 50);
};

var runRightAnimation = function(){
  image(runUse, player.xpos,player.ypos,player.size,player.size);
};

var runLeftAnimation = function(){
  resetMatrix();
    translate(player.xpos, player.ypos);
    scale(-1.0, 1.0
    );
    image(runUse, - walkUse.width + 300,0,player.size,player.size);
    applyMatrix();
};

var jumpRightAnimation = function(){
  image(jumpUse, player.xpos,player.ypos,player.size,player.size);
};

var jumpLeftAnimation = function(){
  resetMatrix();
    translate(player.xpos, player.ypos);
    scale(-1.0, 1.0);
    image(jumpUse, - idleUse.width + 300,0,player.size,player.size);
    applyMatrix();
    
};

var walkLeftAnimation = function(){
    resetMatrix();
    translate(player.xpos, player.ypos);
    scale(-1.0, 1.0);
    image(walkUse, - idleUse.width + 300,0,player.size,player.size);
    applyMatrix();
};

var walkRightAnimation = function(){
  image(walkUse, player.xpos,player.ypos,player.size,player.size);
};

var idleLeftAnimation = function(){
  
    image(idleuse, - idleUse.width + 300,0,player.size,player.size);
  
};

var idleRightAnimation = function(){
  image(idleUse, player.xpos,player.ypos,player.size,player.size);
};

var drawBG = function(){
  image(bg1,0+scrolling,0,Screen.x,Screen.y);
  image(bg1,Screen.x+scrolling,0,Screen.x,Screen.y);
  image(bg1,-Screen.x+scrolling,0,Screen.x,Screen.y);
  image(bg1,(2*Screen.x)+scrolling,0,Screen.x,Screen.y);
  image(bg1,-(2*Screen.x)+scrolling,0,Screen.x,Screen.y);
  
  //BG Scrolling
  if (player. xpos > Screen.x - Screen.x/4){
    if(running){
      scrolling -= player.speed*2;
    }
    else{
      scrolling -= player.speed;
    }
    player.xpos = Screen.x - Screen.x/4;
  }
  if (player.xpos < Screen.x/4){
    if(running){
      scrolling += player.speed*2;
    }
    else{
      scrolling += player.speed;
    }
    player.xpos = Screen.x/4;
  }
};

var drawPlayer = function(){
    


//IDLE ANIMATION LOOP
 idleUse = idle[i];

 if(i < idle.length){
   i++;
 } 
 if(i >= 15){
   i = 1;
 }
 
//WALK ANIMATION LOOP
 walkUse = walk[w];

 if(w < walk.length){
   w++;
 } 
 if(w >= 19){
   w = 1;
 }
 
//JUMP ANIMATION LOOP
 jumpUse = jump[j];

 if(j < jump.length){
   j++;
 } 
 if(j >= 19){
   j = 1;
 }
 
//RUN ANIMATION LOOP
runUse = run[r];

if(r < run.length){
 r++;
} 
if(r >= 19){
 r = 1;
}


};


var playerAnimationController = function(){
  if(right){
    
    if(running){
      if(jumpu || player.ypos != Floor){
        jumpRightAnimation();
      } else {
        runRightAnimation();
      }
      
      player.xpos += player.speed*2;
    }
    else{
      if(jumpu || player.ypos != Floor){
        if(onPlat){
          walkRightAnimation();
        } else {
          jumpRightAnimation();
        }
      }
      else{
        walkRightAnimation();
      }
      player.xpos += player.speed;
    }
    
  }
  
  else if(left){
    
    if(running){
      if(jumpu || player.ypos != Floor){
          jumpLeftAnimation();
      } else {
      runLeftAnimation();
      }
      player.xpos -= player.speed*2;
    }
    else{
      if(jumpu || player.ypos != Floor){
        if(onPlat){
          walkLeftAnimation();
        } else {
          jumpLeftAnimation();
        }
      } 
      else{
        walkLeftAnimation();
      }
      player.xpos -= player.speed;
    }
  }
  
  else if(dirL) {
    
    resetMatrix();
    translate(player.xpos, player.ypos);
    scale(-1.0, 1.0);
        if(jumpu || player.ypos != Floor){
          if(onPlat){
            image(idleUse, - idleUse.width + 300,0,player.size,player.size);
          }
          else{
          image(jumpUse, - jumpUse.width + 300,0,player.size,player.size);
          }
        } 
        else if(running && left){
          image(runUse, - runUse.width + 300,0,player.size,player.size);
        }
        else {
        image(idleUse, - idleUse.width + 300,0,player.size,player.size);
        }
    applyMatrix();
    
  } 
  
  
  else if (dirR) {
    if(jumpu || player.ypos != Floor){
      if(onPlat){
        image(idleUse, player.xpos,player.ypos,player.size,player.size);
      } else{
      image(jumpUse, player.xpos,player.ypos,player.size,player.size);
      }
    } 
    else if(running && right){
      image(runUse, player.xpos,player.ypos,player.size,player.size);
    }
    else {
      image(idleUse, player.xpos,player.ypos,player.size,player.size);
    }
  }
};

var rectCollision = function(obj1, obj2) {
  //First parameter MUST be the player
  if (obj1.xpos + obj1.width > obj2.xpos+scrolling &&
    obj1.xpos < obj2.xpos+scrolling + obj2.width &&
    obj1.ypos + obj1.height > obj2.ypos &&
    obj1.ypos < obj2.ypos + obj2.height) {
      return true;
  } else {
    return false;
  }
};

var circleCollision = function(obj1, obj2){
  var radius1 = obj1.size/2;
  var radius2 = obj2.size/2;
  var circleDistance = dist(obj1.xpos, obj1.ypos, obj2.xpos, obj2.ypos);
  if(radius1 + radius2 > circleDistance){
    return true;
  }
  else{
    return false;
  }

};