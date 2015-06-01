rotate <- function(x) t(apply(x, 2, rev))

path <- paste(getwd(), '/R/heatmap.csv', sep="")
mysurface <- read.csv(path, header=FALSE)
mysurface <- as.matrix(mysurface)

pngPath <- paste(getwd(), '/R/HM.png', sep="")

png(pngPath, width=500, height=500)
filled.contour(x=seq(from=0,to=100,length=20),
y=seq(from=0,to=100,length=20),
z=rotate(mysurface),
nlevels=50,
axes=TRUE,
#col=hsv(h=seq(from=5/6,to=0,length=25),s=1,v=1),
color.palette=topo.colors
)


dev.off()
