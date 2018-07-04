'''
' SnakePrt.BAS
' A snake game where the game progressively gets harder as points
' accumulate via the game's speed.
' This version prints the game as extended ASCII art.
' by: https://github.com/lduran2
' time: 2018-06-23 t11:41
'''

Const INTEGER.SIZE% = 2
Const SINGLE.SIZE% = 4

Const NULL% = 0

Const HeadSection% = 0
Const BodySection% = 1
Const TailSection% = 2

Type SnakeNode
	x as single
	y as single
	section as integer
	nextSeg as integer
	nextPtr as integer
End Type 'SnakeNode

Const SNAKENODE.X.OFF% = 0
Const SNAKENODE.Y.OFF% = SNAKENODE.X.OFF% + SINGLE.SIZE%
Const SNAKENODE.SECTION.OFF% = SNAKENODE.Y.OFF% + SINGLE.SIZE%
Const SNAKENODE.NEXT.SEG.OFF% = SNAKENODE.SECTION.OFF% + INTEGER.SIZE%
Const SNAKENODE.NEXT.PTR.OFF% = SNAKENODE.NEXT.SEG.OFF% + INTEGER.SIZE%

Const REFERENCE.SEGMENT.OFF% = 0
Const REFERENCE.POINTER.OFF% = REFERENCE.SEGMENT.OFF% + INTEGER.SIZE%

Cls

Dim a as SnakeNode
Dim b as SnakeNode

newFirstSnakeNode b, 10, 20, BodySection%
newSnakeNode a, 50, 10, HeadSection%, b

DrawSnake a

'Sub newReference(ref as Reference, segment as integer, pointer as integer)
'	ref.segment = segment
'	ref.pointer = pointer
'End Sub 'newReference(ref as Reference, segment as integer, pointer as integer)

Sub newFirstSnakeNode(snake as SnakeNode, x as single, y as single, section as integer)
	newSnakeNodePtr snake, x, y, section, NULL%, NULL%
End Sub 'Sub newFirstSnakeNode(snake as SnakeNode, x as single, y as single, section as integer)

Sub newSnakeNode(snake as SnakeNode, x as single, y as single, section as integer, nextNode as SnakeNode)
 	newSnakeNodePtr snake, x, y, section, VarSeg(nextNode), VarPtr(nextNode)
End Sub 'Sub newSnakeNode(snake as SnakeNode, x as single, y as single, section as integer, nextNode as SnakeNode)

Sub newSnakeNodePtr(snake as SnakeNode, x as single, y as single, section as integer, segment as integer, pointer as integer)
	snake.x = x
	snake.y = y
	snake.section = section
	snake.nextSeg = segment
	snake.nextPtr = pointer
End Sub 'newSnakeNodePtr(snake as SnakeNode, x as single, y as single, section as integer, segment as integer, pointer as integer)

Sub DrawSnake(snake as SnakeNode)
	DrawSnakePtr VarSeg(snake), VarPtr(snake)
End Sub 'DrawSnake(snake as SnakeNode)

Sub DrawSnakePtr(segment as integer, pointer as integer)
	Dim current as SnakeNode
	Dim tempSegment as Integer

	Dim sectionNames(0 to 2) as String * 4
	sectionNames(HeadSection%) = "Head"
	sectionNames(BodySection%) = "Body"
	sectionNames(TailSection%) = "Tail"

	Do While (1)
		If ((segment = NULL%) AND (pointer = NULL%)) Then
			Exit Do
		End If

		current.x = DerefSng!(segment, pointer + SNAKENODE.X.OFF%)
		current.y = DerefSng!(segment, pointer + SNAKENODE.Y.OFF%)
		current.section = DerefInt%(segment, pointer + SNAKENODE.SECTION.OFF%)
 		tempSegment = DerefInt%(segment, pointer + SNAKENODE.NEXT.SEG.OFF%)
 		pointer = DerefInt%(segment, pointer + SNAKENODE.NEXT.PTR.OFF%)
		segment = tempSegment

		Print current.x, current.y, sectionNames(current.section)
	Loop 'While (1)
End Sub 'DrawSnakePtr(segment as integer, pointer as integer)

Function DerefInt%(segment as integer, pointer as integer)
	DerefInt% = CvI(DerefMkN$(segment, pointer, INTEGER.SIZE%))
End Function 'DerefInt%(segment as integer, pointer as integer)

Function DerefSng!(segment as integer, pointer as integer)
	DerefSng! = CvS(DerefMkN$(segment, pointer, SINGLE.SIZE%))
End Function 'DerefSng!(segment as integer, pointer as integer)

Function DerefMkN$(segment as integer, pointer as integer, size as integer)
	Dim byteStr as String
	Def Seg = segment
	byteStr = String$(size, Chr$(0))
	For k = 1 to size
		Mid$(byteStr, k, 1) = Chr$(Peek(pointer + k - 1))
	Next k
	DerefMkN$ = byteStr
End Function 'DerefMkN$(segment as integer, pointer as integer, size as integer)
