rotate <- function(x) t(apply(x, 2, rev))

mysurface <- read.csv('C:/Users/Yann/Documents/EPFL/3ème année/Projet Bachelor/EyeTracking/Unity/Eye Defense/Assets/heatmap.csv', header=FALSE)
mysurface <- as.matrix(mysurface)

x11()
filled.contour(x=seq(from=0,to=100,length=10),
y=seq(from=0,to=100,length=10),
z=rotate(mysurface),
nlevels=25,
axes=TRUE,
#col=hsv(h=seq(from=5/6,to=0,length=25),s=1,v=1),
color.palette=topo.colors
)


Sys.sleep(10)

#source("C:/Users/Yann/Documents/EPFL/3ème année/Projet Bachelor/EyeTracking/Unity/Eye Defense/R/HeatMap.R")