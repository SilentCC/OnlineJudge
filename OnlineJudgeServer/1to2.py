n,m= map(int, input().split())
n=int(n)
m=int(m)
list1 = []
list1 = input().split()
i = 0
list1.sort()
for i in range(0,n):
    list1[i]=int(list1[i])
i=0
j=n-1

def abc(list1,i,j):
    i=i
    list4 = []
    j=j
    if list1[i]+list1[j]==m:
        list3 = []
        list3.append(list1[i])
        list3.append(list1[j])
        list4.append(list3)
        return i,j,list4
    elif list1[i]+list1[j]<m:
        i+=1
        return abc(list1,i,j)
    else:
        j-=1
        return abc(list1,i,j)



while True:
    result = abc (list1,i,j)
    i=result[0]+1
    j=result[1]-1
    list4=result[2]
    print(list4)
    if i>=j:
        break
