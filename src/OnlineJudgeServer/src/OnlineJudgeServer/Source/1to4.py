def bubble_sort(nums):
  a=0
  for i in range(len(nums) - 1):
    for j in range(len(nums) - i - 1):
      if nums[j] > nums[j + 1]:
        nums[j], nums[j + 1] = nums[j + 1], nums[j]
        a=1
  return nums
b=int(input(''))
nums=input('').split()
for i in range(0,b):
  nums[i]=int(nums[i])
list1=bubble_sort(nums)
for i in range(0,b):
    if i < b-1:
      print(list1[i], end=' ')
    if i==b-1:
      print(list1[i],end='')
print('\n')
