#!/usr/bin/env python
# -*- coding: utf-8 -*-

import  matplotlib.pyplot as plt
import numpy as np
from pylab import mpl
def DrawBar(dicts):
    # print dicts
    mpl.rcParams['font.sans-serif'] = ['FangSong']  # 指定默认字体
    mpl.rcParams['axes.unicode_minus'] = False  # 解决保存图像是负号'-'显示为方块的问题

    vls= []
    labels = []
    for key, value in dicts.items():
        labels.append(key.decode('utf8'))
        vls.append(value)


    n_groups = len(labels)
    # means_men = (20, 35, 30, 35, 27)
    # means_women = (25, 32, 34, 20, 25)

    fig, ax = plt.subplots()
    index = np.arange(n_groups)
    bar_width = 0.35

    opacity = 0.4
    rects1 = plt.bar(index, tuple(vls), bar_width, alpha=opacity, color='b', label=u'板块动态统计')
    # rects2 = plt.bar(index + bar_width, means_women, bar_width, alpha=opacity, color = 'r', label = 'Women')

    plt.xlabel(u'所属板块')
    plt.ylabel(u'新增帖子数量')
    plt.title(u'天涯论坛部分板块')
    plt.xticks(index + bar_width, labels)
    plt.ylim(0, max(vls) + 20)
    plt.legend()

    plt.tight_layout()
    plt.show()
    pass

def startMain2():
    x = np.linspace(-np.pi,np.pi,256,endpoint=True)
    c,s = np.cos(x),np.sin(x)
    plt.figure(1)
    plt.plot(x,c,color="blue",linewidth=1.0,linestyle="-",label="COS",alpha=0.5)
    plt.plot(x,s,"r*",label="SIN")
    plt.title("Cos & Sin")

    ax = plt.gca()
    ax.spines["right"].set_color("none")
    ax.spines["top"].set_color("none")
    ax.spines["left"].set_position(("data",0))
    ax.spines["bottom"].set_position(("data",0))
    ax.xaxis.set_ticks_position("bottom")
    ax.yaxis.set_ticks_position("left")
    plt.xticks([-np.pi,-np.pi / 2,0,np.pi / 2,np.pi],[r'$-\pi$',r'$-\pi/2$',r'$0$',r'$+\pi/2$',r'$+\pi$'])
    plt.yticks(np.linspace(-1,1,5,endpoint=True))

    for label in ax.get_xticklabels() + ax.get_yticklabels():
        label.set_fontsize(16)
        label.set_bbox(dict(facecolor="white",edgecolor="None",alpha=0.2))

    plt.legend(loc="upper left")
    plt.grid()  #

    # plt.axis([-1,1,-0.5,1])

    plt.fill_between(x,np.abs(x) <0.5,c,c>0.5,color="green",alpha=0.25)

    t = 1
    plt.plot([t,t],[0,np.cos(t)],"y",linewidth=3,linestyle="--")

    plt.annotate("cos(1)",xy=(t,np.cos(1)),xycoords="data",xytext=(+10,+30),textcoords="offset points",arrowprops=dict(arrowstyle="->",connectionstyle="arc3,rad=.2"))
    plt.show()
    return

def startMain3():
    # 散点图
    fig = plt.figure()
    fig.add_subplot(3,3,1)
    n = 128
    X = np.random.normal(0,1,n)
    Y = np.random.normal(0,1,n)
    T = np.arctan2(Y,X)
    plt.axes([0.025,0.025,0.95,0.95])
    plt.scatter(X,Y,s=75,c=T,alpha=.5)
    plt.xlim(-1.5,1.5),plt.xticks([])
    plt.ylim(-1.5,1.5),plt.yticks([])
    plt.axis()
    plt.title("Scatter")

    plt.xlabel("X")
    plt.ylabel("Y")

    plt.show()

    return


def startMain():
    # 柱狀圖
    fig = plt.figure()
    ax = fig.add_subplot(3, 3, 1)
    n = 128
    X = np.random.normal(0, 1, n)
    Y = np.random.normal(0, 1, n)
    T = np.arctan2(Y, X)
    # plt.axes([0.025, 0.025, 0.95, 0.95])
    ax.scatter(X, Y, s=75, c=T, alpha=.5)
    plt.xlim(-1.5, 1.5), plt.xticks([])
    plt.ylim(-1.5, 1.5), plt.yticks([])
    plt.axis()
    plt.title("Scatter")

    plt.xlabel("X")
    plt.ylabel("Y")

    fig.add_subplot(332)
    n = 10
    X = np.arange(n)
    Y1 = (1 - X / float(n)) * np.random.uniform(0.5, 1.0, n)
    Y2 = (1 - X / float(n)) * np.random.uniform(0.5, 1.0, n)

    plt.bar(X, +Y1,facecolor="#9999ff",edgecolor="white")
    plt.bar(X, -Y2, facecolor="#9999ff", edgecolor="white")

    for x,y in zip(X,Y1):
        plt.text(x + 0.4,y + 0.05,'%.2f'% y, ha= "center",va="bottom")

    for x,y in zip(X,Y2):
        plt.text(x + 0.4, y - 0.05, '%.2f' % y, ha="center", va="top")


    # 饼图
    ax = fig.add_subplot(333)
    n = 20
    Z = np.ones(n)
    ax.pie(Z,explode=Z*.05,colors=['%f'%(i/float(n)) for i in range(n)],labels=['%.2f'%(i/float(n)) for i in range(n)])
    plt.gca().set_aspect("equal")
    plt.xticks([]),plt.yticks([])


    # 及坐标
    ax = fig.add_subplot(334,polar=True)
    n=20
    theta=np.arange(0.0,2*np.pi,2*np.pi/n)
    radii=10*np.random.rand(n)
    plt.plot(theta,radii)

    # 热图
    ax = fig.add_subplot(335)
    from matplotlib import  cm
    data = np.random.rand(3,3)
    cmap=cm.Reds
    map = plt.imshow(data,interpolation="nearest",cmap=cmap,aspect='auto',vmin=0,vmax = 1)

    # 3D图
    from  mpl_toolkits.mplot3d import  Axes3D
    ax = fig.add_subplot(336,projection="3d")
    ax.scatter(1,1,3,s=100)


    # 热力图
    fig.add_subplot(3,1,3)
    def f(x,y):
        return  (1-x/2 + x**5 + y**3)*np.exp(-x**2-y**2)

    n=256
    x=np.linspace(-3,3,n)
    y=np.linspace(-3,3,n)
    X,Y = np.meshgrid(x,y)
    plt.contourf(X,Y,f(X,Y),8,alpha=.75, cmap=plt.cm.hot)

    plt.savefig("d:/test.jpg")
    plt.show()

    return
if __name__ == "__main__":
    dicts={}
    dicts["A"] = 10
    dicts["B"] = 18
    dicts["C"] = 91
    DrawBar(dicts)
