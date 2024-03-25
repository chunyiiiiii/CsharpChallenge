#1.构造函数和属性：

构造函数 BigNumber(string s) 接受一个字符串参数，将字符串转换为大整数表示，存储在 bits 列表中。
属性 Length 返回大整数的位数。

#2.运算符重载：

重载了加法 +、减法 -、乘法 *、除法 /、取模 % 等运算符，实现了大整数的基本运算。

#2.1 加法 + 运算符重载
在 operator + 中，创建一个新的 BigNumber 对象 result 用于存储运算结果。
遍历两个大整数 bn1 和 bn2 中的每一位，相加，并将结果添加到 result 的 bits 列表中。
最后调用 Norminalize 方法规范化结果，处理进位等情况，确保结果正确。

#2.2 减法 - 运算符重载
在 operator - 中，创建一个新的 BigNumber 对象 result 用于存储运算结果。
确定被减数和减数，保证被减数大于等于减数，若不满足则进行交换。
遍历两个大整数 bn1 和 bn2 中的每一位，进行减法运算，并处理借位情况。
最后调用 Norminalize 方法规范化结果，确保结果正确，并根据需要设置结果为负数。

#2.3 乘法 * 运算符重载
在 operator * 中，创建一个新的 BigNumber 对象 result 用于存储运算结果。
初始化 result 的 bits 列表为零，并遍历两个大整数 bn1 和 bn2 的每一位进行乘法操作。
在内层循环中，将乘积添加到正确位置，并及时调用 Norminalize 方法处理进位情况。
最后再次调用 Norminalize 方法，确保结果正确。
重载了比较运算符 >=、<=、>、<、==、!=，用于比较两个大整数的大小和相等性。

#2.4 除法 / 运算符重载
在 operator / 中，创建一个新的 BigNumber 对象 result 用于存储运算结果，另外创建两个临时对象 remainder 和 divisor。
判断除数是否为零，若为零则抛出异常。
使用循环将被除数减去除数直到被除数小于除数，记录商并更新被除数。
将商添加到 result 的 bits 列表中，并保持规范化。

#2.5 取模 % 运算符重载
在 operator % 中，创建一个新的 BigNumber 对象 remainder 用于存储取模运算的结果。
判断除数是否为零，若为零则抛出异常。
使用循环将被除数减去除数直到被除数小于除数，最终得到余数。

#3.运算方法：

Norminalize(bool removeZeros) 方法用于规范化大整数，去除前导零。
operator * 实现大整数乘法，采用竖式乘法。
operator + 实现大整数加法。
operator - 实现大整数减法。
operator / 实现大整数除法。
operator % 实现大整数取模。

#4.比较方法：

通过重载比较运算符实现大整数的比较操作，包括大于、小于、等于等情况。

#5.ToString 方法：

重写 ToString 方法，将大整数转换为字符串表示，考虑了负数情况。
#6.Main 方法：

在 Main 方法中，创建两个大整数对象 bn1 和 bn2，进行加法、减法、乘法、除法和取模运算，然后输出结果。
