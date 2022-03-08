

def FiboToRecursive(num):
    if num==1 or num==2:
        return 1
    else:
        return FiboToRecursive(num-1)+FiboToRecursive(num-2)

def FiboToDefault(num):
    a,b=1,1
    if num ==1 or num==2:
        return 1
    for i in range(1,num):
        a,b = b,a+b
    return a

for x in range(1,10):
    print(" ",FiboToDefault(x),end='')
print("")
for x in range(1,10):
    print(" ",FiboToRecursive(x),end='') 