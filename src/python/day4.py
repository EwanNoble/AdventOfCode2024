#!/usr/bin/env python3

def main():
    with open('inputs/day4.txt', 'r') as file:
        data = file.read()

    lines = data.splitlines()

    part1total = 0
    part2total = 0
    array = [list(line) for line in lines]

    height = len(lines)
    for y, row in enumerate(array):
        width = len(row)
        for x, element in enumerate(row):
            if(element == "X"):
                # n
                if(y - 3 >= 0 and array[y-1][x] == "M" and array[y-2][x] == "A" and array[y-3][x] == "S"):
                    part1total += 1

                # ne
                if(y - 3 >= 0 and x + 4 <= width and array[y-1][x+1] == "M" and array[y-2][x+2] == "A" and array[y-3][x+3] == "S"):
                    part1total += 1

                # e
                if(x + 4 <= width and array[y][x+1] == "M" and array[y][x+2] == "A" and array[y][x+3] == "S"):
                    part1total += 1

                # se
                if(x + 4 <= width and y + 4 <= height and array[y+1][x+1] == "M" and array[y+2][x+2] == "A" and array[y+3][x+3] == "S"):
                    part1total += 1

                # s
                if(y + 4 <= height and array[y+1][x] == "M" and array[y+2][x] == "A" and array[y+3][x] == "S"):
                    part1total += 1

                # sw
                if(y + 4 <= height and x - 3 >= 0 and array[y+1][x-1] == "M" and array[y+2][x-2] == "A" and array[y+3][x-3] == "S"):
                    part1total += 1

                # w
                if(x - 3 >= 0 and array[y][x-1] == "M" and array[y][x-2] == "A" and array[y][x-3] == "S"):
                    part1total += 1

                # nw
                if(x - 3 >= 0 and y - 3 >= 0 and array[y-1][x-1] == "M" and array[y-2][x-2] == "A" and array[y-3][x-3] == "S"):
                    part1total += 1
            elif(element == "A" and x > 0 and y > 0 and y < height-1 and x < width-1):
                cross1=(array[y-1][x-1]=="M" and array[y+1][x+1]=="S") or (array[y-1][x-1]=="S" and array[y+1][x+1]=="M")
                cross2=(array[y+1][x-1]=="M" and array[y-1][x+1]=="S") or (array[y+1][x-1]=="S" and array[y-1][x+1]=="M")
                if(cross1 and cross2):
                    part2total += 1

    print(part1total)
    print(part2total)



if __name__ == "__main__":
    main()