const scoreEl = document.querySelector('#scoreEl')
const canvas = document.querySelector('canvas')
const c = canvas.getContext('2d')


canvas.width = innerWidth
canvas.height = innerHeight

class Player{
    constructor() {

        this.velocity = {
            x: 0,
            y: 0
        }
        this.rotation=0

        const image = new Image()
        image.src = './sourc/starship.png'
        image.onload = () => {
            const scale = 0.35
            this.image = image
            this.width = image.width * scale
            this.height = image.height * scale
            this.position = {
                x:canvas.width/2 - this.width / 2,
                y:canvas.height - this.height - 25
            }
        }
    }

    draw() {
        //c.fillStyle = 'black'
        //c.fillRect(this.position.x, this.position.y, this.width, this.height)
        c.save()
        c.translate(player.position.x + player.width/2, player.position.y + player.height/2)
        c.rotate(this.rotation)
        c.translate(-player.position.x - player.width/2, -player.position.y - player.height/2)
        if (this.image)
            c.drawImage(this.image, this.position.x, this.position.y, this.width, this.height)
        c.restore()
    }

    update() {
        if (this.image) {
            this.draw()
            this.position.x += this.velocity.x
        }
    }
}

class Projectile{
    constructor({position,velocity}){
        this.position=position
        this.velocity=velocity
        this.radius=5
    }

    draw(){
        c.beginPath()
        c.arc(this.position.x, this.position.y, this.radius, 0, Math.PI *2)
        c.fillStyle='red'
        c.fill()
        c.closePath()
    }
    update(){
        this.draw()
        this.position.x+= this.velocity.x
        this.position.y+=this.velocity.y
    }
}

class InvaderProjectile{
    constructor({position,velocity}){
        this.position=position
        this.velocity=velocity

        this.width =3
        this.heigth =10
    }

    draw(){
        c.fillStyle = 'white'
        c.fillRect(this.position.x,this.position.y,this.width,this.height)
    }
    update(){
        this.draw()
        this.position.x+= this.velocity.x
        this.position.y+=this.velocity.y
    }
    
}

class Invader{
    constructor({position}) {

        this.velocity = {
            x: 0,
            y: 0
        }

        const image = new Image()
        image.src = './sourc/enemy.png'
        image.onload = () => {
            const scale = 0.05
            this.image = image
            this.width = image.width * scale
            this.height = image.height * scale
            this.position = {
                x: position.x,
                y: position.y 
            }
        }
    }

    draw() {
        //c.fillStyle = 'black'
        //c.fillRect(this.position.x, this.position.y, this.width, this.height)
        c.drawImage(this.image, this.position.x, this.position.y, this.width, this.height)
    }

    update({velocity}) {
        if (this.image) {
            this.draw()
            this.position.x += velocity.x
            this.position.y += velocity.y
        }
    }
    shoot(invaderProjectiles){
        invaderProjectiles.push(new InvaderProjectile({
            position: {
                x: this.position.x + this.width/2, 
                y: this.position.y + this.height
            },
            velocity: {
                x: 0,
                y: 5
            }
        }))
    }
}

class Grid {
    constructor(){
        this.position = {
            x: 0,
            y: 0
        }
        this.velocity = {
            x: 3,
            y: 0
        }
        this.invaders = []
        const rows = Math.floor(Math.random() * 5 + 2)
        const columns = Math.floor(Math.random() * 10 + 5)

        this.width = columns * 60

        for (let x =0; x< columns;x++){
            for (let y =0; y< rows;y++){
            this.invaders.push(new Invader({position:{
             x: x * 60,
             y: y * 60
        }}))}
    }
        console.log(this.invaders)}

    update() {
        this.position.x +=this.velocity.x
        this.position.y +=this.velocity.y
        this.velocity.y = 0

        if (this.position.x+ this.width >= canvas.width || this.position.x <=0){
            this.velocity.x= -this.velocity.x
            this.velocity.y= 120
        }
    }
}
    
const player = new Player()
const projectiles =[]
const grids = []
const invaderProjectiles = []
const keys={
    a:{
        pressed: false
    },
    d:{
        pressed: false
    },
    space: {
        pressed: false
    }
}

let frames=0
let randomInterval = Math.floor((Math.random() * 500) + 500)
let score = 0

function animate() {
    requestAnimationFrame(animate)
    c.fillStyle = 'black'
    c.fillRect(0,0,canvas.width, canvas.height)
    player.update()
    invaderProjectiles.forEach((invaderProjectile) =>{
    invaderProjectile.update()
    })
    projectiles.forEach((projectile, index) => {
        if (projectile.position.y + projectile.radius <=0) {
            setTimeout(() => {
                projectiles.splice(index,1)},0)
        } else {
        projectile.update()
        }
    })
    grids.forEach((grid, gridIndex) => {
        grid.update()
        if (frames % 100 === 0 && grid.invaders.length > 0){
            grid.invaders[Math.floor(Math.random() * grid.invaders.length)].shoot(invaderProjectiles)
            console.log(frames)
        }
        grid.invaders.forEach((invader, i) => {
            invader.update({velocity: grid.velocity})

            projectiles.forEach((projectile, j) => {
                if (projectile.position.y - projectile.radius <= invader.position.y + invader.height && projectile.position.x - projectile.radius <= invader.position.x + invader.width  && projectile.position.x - projectile.radius <=invader.position.x  ){
                    setTimeout(() => {
                        const invaderFound = grid.invaders.find((invader2) =>
                            invader2 === invader
                        )
                        const projectileFound = projectiles.find((projectile2) => projectile2 === projectile)
                        if (invaderFound && projectileFound){
                        score +=100
                        console.log(score)
                        scoreEl.innerHTML = score
                        grid.invaders.splice(i, 1)
                        projectiles.splice(j, 1)
                        if (grid.invaders.length > 0){
                            const firstInvader = grid.invaders[0]
                            const lastInvader = grid.invaders[grid.invaders.length - 1]

                            grid.width= lastInvader.position.x - firstInvader.position.x + lastInvader.width
                            grid.position.x = firstInvader.position.x
                        } else {
                            grids.splice(gridIndex, 1)
                        }
                    }
                    }, 0)
                }
            })
        })
    })
    if (this.frames % 10000 === 0) {
                $.ajax({
                    method: "POST",
                    url: '/GameRecords/Add',
                    data: { score: score }
                });
                perish();
            }
    if (keys.a.pressed && player.position.x>=0){
        player.velocity.x =-10
        player.rotation =- 0.15
    } else if (keys.d.pressed && player.position.x+player.width<=canvas.width){
        player.velocity.x =10
        player.rotation =+ 0.15
    } else{
        player.velocity.x=0
        player.rotation = 0
    }
    if (frames % randomInterval === 0){
        grids.push(new Grid())
        randomInterval = Math.floor(Math.random()* 500 + 500)
        frames = 0
    }
    frames++
    }


animate()

addEventListener('keydown', ({key}) => {
    switch(key) {
        case 'a':
            console.log('left')
            keys.a.pressed=true
            break
        case 'd':
            console.log('right')
            keys.d.pressed=true
            break
        case ' ':
            console.log('space')
            projectiles.push(
                new Projectile({
                    position: {
                        x: player.position.x + player.width/2,
                        y: player.position.y
                },
                    velocity:{
                        x: 0,
                        y: -5
                }
            })
            )
            console.log(projectiles)
            break

    }
})

addEventListener('keyup', ({key}) => {
    switch(key) {
        case 'a':
            console.log('left')
            keys.a.pressed=false
            break
        case 'd':
            console.log('right')
            keys.d.pressed=false
            break
        case ' ':
            console.log('space')
            break

    }
})
